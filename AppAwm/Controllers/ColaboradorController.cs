using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using QRCoder;
using System.Drawing;
using System.Text.RegularExpressions;
using X.PagedList.Extensions;

namespace AppAwm.Controllers
{
    public class ColaboradorController(IColaborador<ColaboradorAnswer> _servico) : Controller
    {
        private readonly IColaborador<ColaboradorAnswer> servico = _servico;


        [Authorize(Roles = "Analista, Terceiro, Administrador")]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Start");

            var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

            List<SelectListItem> selectListItems = [.. servico.GetEmpresas(s =>
            (userSession!.Perfil == EnumPerfil.Administrador ? s.Cd_Empresa > 0 : s.Cd_Empresa == userSession.Cd_Empresa) && s.Status)
                .Select(s => new SelectListItem { Text = s.Nome, Value = s.Cd_Empresa.ToString() }).OrderBy(o => o.Text)];
            ViewData["selectsEmpresa"] = selectListItems;
            return View();
        }

        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public ActionResult Create(int id)
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Start");

            var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

            ColaboradorAnswer funcionarioAnswer = servico.Get(p =>
            p.Cd_Funcionario == id
            && p.Cd_UsuarioCriacao == (userSession!.Perfil == EnumPerfil.Administrador ? p.Cd_UsuarioCriacao : userSession.Nome!)
            , userSession);

            return View(funcionarioAnswer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public ActionResult Create(Colaborador colaborador)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Start");

                ModelState.Remove("Cd_Funcionario");

                Colaborador? checkColaborador =  null;

                if (ModelState.IsValid)
                {
                    var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                    bool isNovosuario = colaborador.Cd_Funcionario == 0;

                    if (!servico.CheckClienteVidasDisponivel())
                        return BadRequest(ColaboradorAnswer.DeErro("Não é possivel cadastrar esse colaborador, pois excede o total de vidas dispnivel no pacote do cliente."));

                    if (colaborador.Cd_Funcionario > 0)
                    {
                        checkColaborador = servico.Get(g => g.Cd_Funcionario == colaborador.Cd_Funcionario, userSession).Colaborador;

                        colaborador.Cd_UsuarioAtualizacao = User.Identity?.Name ?? "ANONYMOUS";
                        colaborador.Dt_Atualizacao = DateTime.Now;
                        colaborador.Cd_UsuarioCriacao = colaborador.Cd_UsuarioCriacao;
                        colaborador.Cd_UsuarioAtualizacao = colaborador.Cd_UsuarioAtualizacao.PadRight(30, '*')[..30].Replace("*", string.Empty).Trim();
                    }
                    else
                    {
                        colaborador.Cd_UsuarioCriacao = User.Identity?.Name ?? "ANONYMOUS";
                        colaborador.Id_UsuarioCriacao = userSession!.Cd_Usuario;
                        colaborador.Cd_UsuarioCriacao = colaborador.Cd_UsuarioCriacao.PadRight(30, '*')[..30].Replace("*", string.Empty).Trim();

                        string doc = Regex.Replace(colaborador.Documento!, @"[^\d]", string.Empty);

                        bool check = servico.Check(s => s.Documento!.Equals(doc));

                        if (check)
                            return BadRequest(EmpresaAnswer.DeFalha("CPF já cadastrado"));
                    }

                    colaborador.Documento = Regex.Replace(colaborador.Documento!, @"[^\d]", string.Empty);

                    ColaboradorAnswer funcionarioAnswer = servico.Save(colaborador, (colaborador.Cd_Funcionario == 0 ? EnumAcao.Criar : EnumAcao.Editar));

                    if (funcionarioAnswer.Success)
                    {
                        int resposta = 0;

                        if (isNovosuario)
                            resposta = servico.UpdateCliente(true);
                        else
                        {
                            if (colaborador.Status != checkColaborador!.Status)
                                resposta = servico.UpdateCliente(colaborador.Status);
                        }
                    }

                    return funcionarioAnswer.Success ? Ok(funcionarioAnswer) : BadRequest(funcionarioAnswer);
                }

                string[] messages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(ColaboradorAnswer.DeErro(messages));
            }
            catch (Exception ex)
            {
                return BadRequest(ColaboradorAnswer.DeErro(ex.Message));
            }
        }


        //[HttpGet("search")]
        //[Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        //public ActionResult GetCep(string cep)
        //{
        //    var endereco =  Utility.GetCepAsync(cep).Result;
        //    return Ok(endereco);
        //}

        [HttpGet("/Colaborador/getAutoComplete")]
        public ActionResult GetCargosAutoComplete(string _campo)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Start");

            List<Cargo> lstCargos = servico.GetCargos(_campo);

            List<KeyValuePair<int, string>> lst = new List<KeyValuePair<int, string>>();

            lstCargos.ForEach(f => lst.Add(new(f.Cd_Cargo, f.Nome!)));

            return Ok(lst);
        }

        [HttpGet]
        [Route("/Colaborador/Search/{skip:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public PartialViewResult? Search(string funcionario, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                Colaborador obj = JsonConvert.DeserializeObject<Colaborador>(funcionario) ?? new();

                if (!string.IsNullOrWhiteSpace(obj.Documento))
                    obj.Documento = Regex.Replace(obj.Documento, @"[^0-9$]", string.Empty);

                ColaboradorAnswer resposta = servico.List(
                     x => (x.Nome!.ToUpper().StartsWith(obj.Nome!.ToUpper())
                     && x.Id_UsuarioCriacao == (userSession!.Perfil == EnumPerfil.Administrador ? x.Id_UsuarioCriacao : userSession.Cd_Usuario)
                     && x.Documento!.StartsWith(obj.Documento ?? x.Documento)
                     && (x.Id_Empresa == (obj.Id_Empresa == 0 ? x.Id_Empresa : obj.Id_Empresa))
                     && (x.TipoContrato == (obj.TipoContrato == 0 ? x.TipoContrato : obj.TipoContrato))
                     && (obj.StatusFilter.HasValue ? x.Status == obj.StatusFilter > 0 : x.Status == x.Status)));

                var query = resposta.Colaboradores.ToPagedList(skip, 12);
                return PartialView("ListRecord", query);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/Colaborador/qrcode/{Id:int}")]
        public ActionResult ViewQrCode(int Id)
        {
            try
            {
                Cracha resposta = servico.GetCracha(Id);
                return View("ViewQrCode", resposta);
            }
            catch
            {
                throw;
            }
        }


        [HttpGet]
        [Route("/Colaborador/Cracha/{Id:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public PartialViewResult GetCracha(int Id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("PartilCracha", BadRequest("Usuario não autenticado"));

                Cracha resposta = servico.GetCracha(Id);

                if (resposta == null)
                    return PartialView("PartilCracha", resposta);

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode($"https://{Request.Host}/Colaborador/qrcode/{Id}", QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                resposta.QrCode = BitmapToBytes(qrCodeImage);

                return PartialView("PartilCracha", resposta);
            }
            catch
            {
                throw;
            }
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using MemoryStream stream = new();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }


        [HttpPost]
        [Route("/Colaborador/importar")]
        [Authorize(Roles = "Terceiro, Administrador")]
        public async Task<IActionResult> ImportarColaborador(List<IFormFile> files, string cd_empresa)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Start");

                Empresa? empresa = null;
                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                var formFile = files[0];

                if(!formFile.FileName.Split('.').Any(a => a == "xlsx" ))
                    return Json(ColaboradorAnswer.DeErro("O tipo do arquivo informado, não é compativel para realizar a importação<br/>somente arquivos com a extensão .xlsx"));

                using var stream = new MemoryStream();
                await formFile.CopyToAsync(stream);

                ColaboradorAnswer resposta = servico.ImportarColaboradore(stream, userSession, Convert.ToInt32(cd_empresa));
                
                //if(resposta.Success)
                //    empresa = servico.GetEmpresas(p => p.Cd_Empresa == Convert.ToInt32(cd_empresa)).FirstOrDefault();

                return Json(resposta.Success ? resposta : ColaboradorAnswer.DeErro(resposta.Message));
            }
            catch(Exception ex)
            {
                return Json(ColaboradorAnswer.DeErro($"Ocorreu um erro na importação de colaboradores {ex.Message}"));
            }
        }

    }
}
