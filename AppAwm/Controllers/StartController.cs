using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace AppAwm.Controllers
{
    public class StartController(IConfiguration section) : Controller
    {

        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;
        private IConfiguration configurationSection = section;

        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("Windows")]
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Index(Login login)
        {
            ArgumentNullException.ThrowIfNull(login);
            try
            {
                if (ModelState.IsValid)
                {

                    using DbCon db = new();
                    using var contexto = new RepositoryGeneric<Usuario>(db, out status);

                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _Usuario = contexto.GetAll(n => n.Login == login.UserName && n.Senha == Util.Utility.Criptografar(login.Password!) && n.Perfil == login.Perfil && n.Status).FirstOrDefault();

                        if (_Usuario is null)
                        {
                            if (login.Operacao)
                                return new JsonResult(new { message = "Falha no login", success = false });

                            ViewBag.invalido = true;
                            return View(login);
                        }

                        if (_Usuario.Status)
                        {

                            var contextoCliente = new RepositoryGeneric<Empresa>(db, out status);

                            var empresaCliente = contextoCliente.GetAll(cli => cli.Cd_Empresa == _Usuario.Cd_Empresa).Include(cl => cl.Cliente).FirstOrDefault();
                            Usuario user = _Usuario;
                            user.Empresa = empresaCliente;
                            user.Empresa!.Complemento = null;

                            if (empresaCliente is not null)
                            {
                                Utility.DocumentacaoComplementares = [.. db.DocumentacoesComplementares];
                                Utility.Cliente = _Usuario.Cliente = empresaCliente.Cliente;
                                _Usuario.Cliente!.Empresas!.Clear();
                            }

                            if (login.Operacao)
                                return new JsonResult(_Usuario);

                            var token = Utility.GenerateToken(new Usuario { Login = login.UserName, Nome = _Usuario.Nome, Email = _Usuario.Email ?? login.UserName, Perfil = login.Perfil, Telefone = _Usuario.Telefone });
                            HttpContext.Session.SetString("Token", token);
                            HttpContext.Session.SetString("UserAuth", JsonSerializer.Serialize(user));


                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                        else
                        {
                            if (login.Operacao)
                                return new JsonResult(new { message = "Usuario inativo/bloqueado", success = false });

                            ViewBag.invalido = true;
                            return View(login);
                        }

                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else

                    return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "/");
        }



        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("solicitaNovapwd")]
        public IActionResult SolicitarNovaSenha(string documento)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Usuario>(db, out status);

                documento = Regex.Replace(documento, @"[^0-9$]", string.Empty);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var _usuario = contexto.GetItem(n => n.Documento == documento);

                    if (_usuario == null)
                        return Json(new { cpf = documento, success = false, message = " Usuário não localizado." });

                    if (!_usuario.Status)
                        return Json(new { cpf = documento, success = false, message = $" O usuário {_usuario.Nome} está bloqueado." });

                    _usuario.Senha = Util.Utility.Criptografar("$123Master");
                    _usuario.MudarSenha = true;
                    _usuario.Dt_Atualizacao = DateTime.Now.Date;
                    _usuario.Cd_Usuario_Atualizacao = User.Identity.Name;

                    int ret = contexto.Edit(_usuario);

                    if (ret > 0)
                        Utility.EnviarEmail(false, _usuario);

                    string msg = ret > 0 ? "Dados enviados para" : "Não foi possivel eniar os dados de acesso, contate o adminitrador.";

                    return Json(new { cpf = documento, success = ret > 0, message = $" {msg} - {_usuario.Email}." });
                }

                return Json(new { cpf = documento, success = false, message = $"não foi possivel estabelecer comunicação com a base de dados." });
            }
            catch (Exception ex)
            {
                return Json(new { cpf = documento, success = false, message = $"Ocorreu um ERRO: {ex.Message}." });
            }

        }


        [HttpPost("/Start/changePassword")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePwd(Usuario usuario)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Usuario>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    int ret = contexto.GetAll(u => u.Cd_Usuario == usuario.Cd_Usuario)
                        .ExecuteUpdate(up => up
                        .SetProperty(p => p.Senha, Util.Utility.Criptografar(usuario.Senha!))
                        .SetProperty(p => p.MudarSenha, false));

                    return Json(new { success = (ret > 0), message = (ret > 0) ? "Senha alterada com sucesso!" : "A senha não pode ser alterada, comunique ao administrador do sistema" });
                }

                return Json(new { success = false, message = "Não foi possivel contectar com o banco de dados" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Ocorreu um ERRO: {ex.Message}" });
            }
        }
    }
}
