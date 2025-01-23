using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using AppAwm.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using X.PagedList.Extensions;

namespace AppAwm.Controllers
{
    [Authorize]
    public class UsuarioController(IUsuario<UsuarioAnswer> _servico) : Controller
    {
        private readonly IUsuario<UsuarioAnswer> servico = _servico;

        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public IActionResult Create(Usuario usuario)
        {
            try
            {
               // Utility.EnviarEmail(true, new Usuario { Email = "agles.net@msn.com", Nome="agles silva" }, "http://hdsfasd.local.teste");

                ModelState.Remove("usuario.Cd_Usuario");

                if (string.IsNullOrEmpty(usuario.Senha))
                {
                    ModelState.Remove("usuario.Senha");
                    ModelState.Remove("usuario.ConfirmPassword");
                }

                if (ModelState.IsValid)
                {
                    var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                    usuario.Documento = Regex.Replace(usuario.Documento!, @"[^\d]", string.Empty);

                    if (usuario.Cd_Usuario == 0)
                    {
                        UsuarioAnswer check = servico.Get(w => w.Documento == usuario.Documento, EnumAcao.Nenhum);

                        if (check.Usuario.Cd_Usuario > 0)
                        {
                            var exists = UsuarioAnswer.Falha($"Usuário com cpf: {usuario.Documento} já registrado", EnumAcao.Criar);
                            return BadRequest(exists);
                        }

                        usuario.Senha = Utility.Criptografar(usuario.Senha!);
                        usuario.Cd_Usuario_Criacao = User.Identity!.Name;
                        usuario.MudarSenha = true;

                        var retorno = servico.Save(usuario, EnumAcao.Criar);

                        if (retorno.Success)
                        {
                            string url = Request.Scheme + "://" + Request.Host;

                            Utility.EnviarEmail(true, usuario, url);
                            retorno.Usuario.Senha = usuario.Senha;
                            return Ok(retorno);
                        }
                    }
                    else
                    {
                        var obj = servico.Get(n => n.Cd_Usuario == usuario.Cd_Usuario, EnumAcao.Editar);
                        obj.Usuario.Cd_Usuario_Atualizacao = User.Identity!.Name;
                        obj.Usuario.Dt_Atualizacao = DateTime.Now.Date;
                        obj.Usuario.Status = usuario.Status;
                        obj.Usuario.Perfil = usuario.Perfil;
                        obj.Usuario.Email = usuario.Email;
                        obj.Usuario.Nome = usuario.Nome;
                        obj.Usuario.Telefone = usuario.Telefone;
                        obj.Usuario.Cd_Empresa = usuario.Cd_Empresa;
                        obj.Usuario.Senha = string.IsNullOrWhiteSpace(usuario.Senha) ? obj.Usuario.Senha : Utility.Criptografar(usuario.Senha!);

                        obj = servico.Save(obj.Usuario, EnumAcao.Editar);

                        if (obj.Success)
                        {
                            obj.Usuario.Senha = usuario.Senha;
                            return Ok(obj);
                        }

                        return BadRequest(obj);
                    }

                }
                string[] messages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(UsuarioAnswer.DeErro(messages));
            }
            catch (Exception ex)
            {
                return BadRequest(UsuarioAnswer.DeErro(ex.Message));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create(int id)
        {
            try
            {
                List<SelectListItem> selectListItems = [.. servico.GetEmpresas(s => s.Status).Select(g => new SelectListItem { Value = g.Cd_Empresa.ToString(), Text = g.Nome }).OrderBy(t => t.Text)];
                ViewData["selectCliente"] = selectListItems;

                if (id > 0)
                {
                    var _usuario = servico.Get(n => n.Cd_Usuario == id, EnumAcao.Nenhum);
                    if (_usuario.Success)
                    {
                        return View(_usuario);
                    }
                    return View(UsuarioAnswer.DeErro("Usuário não localizado"));
                }
                else
                    return View();
            }
            catch (Exception ex)
            {
                return View(UsuarioAnswer.Falha(ex.Message, EnumAcao.Nenhum));
            }
        }

        [HttpGet]
        [Route("/Usuario/Search/{skip:int}")]
        [Authorize(Roles = "Administrador")]
        public PartialViewResult Search(string usuario, int skip = 1)
        {
            try
            {
                Usuario? obj = JsonConvert.DeserializeObject<Usuario>(usuario);

                UsuarioAnswer resposta = servico.List(
                     x => x.Nome!.ToUpper().StartsWith(obj.Nome.ToUpper()) && (obj.StatusFilter.HasValue ? x.Status == obj.StatusFilter > 0 : x.Status == x.Status));

                var query = resposta.Usuarios.ToPagedList(skip, 12);
                return PartialView("ListRecord", query);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
