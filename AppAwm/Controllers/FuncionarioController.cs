using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using AppAwm.Util;
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
    public class FuncionarioController(IFuncionario<FuncionarioAnswer> _servico) : Controller
    {
        private readonly IFuncionario<FuncionarioAnswer> servico = _servico;


        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Start");

            var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

            List<SelectListItem> selectListItems = [.. servico.GetEmpresas(s => 
            s.Cd_UsuarioCriacao == (userSession!.Perfil == EnumPerfil.Administrador ? s.Cd_UsuarioCriacao : userSession.Nome) && s.Status)
                .Select(s => new SelectListItem { Text = s.Nome, Value = s.Cd_Empresa.ToString() }).OrderBy(o => o.Text)];
            ViewData["selectsEmpresa"] = selectListItems;
            return View();
        }

        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public ActionResult Create(int id)
        {

            if (!User.Identity.IsAuthenticated)
                return View(BadRequest("Usuario não autenticado"));

            var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

            FuncionarioAnswer funcionarioAnswer =  servico.Get(p => 
            p.Cd_Funcionario == id
            && p.Cd_UsuarioCriacao == (userSession!.Perfil == EnumPerfil.Administrador ? p.Cd_UsuarioCriacao : userSession.Nome!) && p.Status
            , userSession);

            return View(funcionarioAnswer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public ActionResult Create(Funcionario funcionario)
        {
            try
            {
                ModelState.Remove("Cd_Funcionario");
                ModelState.Remove("Endereco.Cd_Endereco");
                ModelState.Remove("Endereco.Cd_Empresa");
                ModelState.Remove("Endereco.Cd_Funcionario_id");

                if (ModelState.IsValid)
                {
                    if (funcionario.Cd_Funcionario > 0)
                    {
                        funcionario.Cd_UsuarioAtualizacao = User.Identity?.Name ?? "ANONYMOUS";
                        funcionario.Dt_Atualizacao = DateTime.Now;
                        funcionario.Endereco.Dt_Atualizacao = DateTime.Now;
                        funcionario.Endereco.Cd_UsuarioAtualizacao = User.Identity?.Name ?? "ANONYMOUS";
                    }
                    else
                    {
                        funcionario.Cd_UsuarioCriacao = User.Identity?.Name ?? "ANONYMOUS";
                        funcionario.Endereco.Cd_UsuarioCriacao = User.Identity?.Name ?? "ANONYMOUS";

                        string doc = Regex.Replace(funcionario.Documento!, @"[^\d]", string.Empty);

                        bool check = servico.Check(s => s.Documento!.Equals(doc));

                        if (check)
                            return BadRequest(EmpresaAnswer.DeFalha("CPF já cadastrado"));
                    }

                    funcionario.Documento = Regex.Replace(funcionario.Documento!, @"[^\d]", string.Empty);

                    FuncionarioAnswer funcionarioAnswer = servico.Save(funcionario, (funcionario.Cd_Funcionario == 0 ? EnumAcao.Criar : EnumAcao.Editar));

                    return funcionarioAnswer.Success ? Ok(funcionarioAnswer) : BadRequest(funcionarioAnswer);
                }

                string[] messages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(FuncionarioAnswer.DeErro(messages));
            }
            catch(Exception ex)
            {
                return BadRequest(FuncionarioAnswer.DeErro(ex.Message));
            }
        }


        [HttpGet("search")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public ActionResult GetCep(string cep)
        {
            var endereco =  Utility.GetCepAsync(cep).Result;
            return Ok(endereco);
        }

        [HttpGet("/Funcionario/getAutoComplete")]
        public ActionResult GetCargosAutoComplete(string _campo)
        {
            List<Cargo> lstCargos = servico.GetCargos(_campo);

            List<KeyValuePair<int, string>> lst = new List<KeyValuePair<int, string>>();

            lstCargos.ForEach(f =>  lst.Add(new(f.Cd_Cargo, f.Nome!)));

            return Ok(lst);
        }

        [HttpGet]
        [Route("/Funcionario/Search/{skip:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public PartialViewResult? Search(string funcionario, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                Funcionario obj = JsonConvert.DeserializeObject<Funcionario>(funcionario) ?? new();

                if (!string.IsNullOrWhiteSpace(obj.Documento))
                    obj.Documento= Regex.Replace(obj.Documento, @"[^0-9$]", string.Empty);

                FuncionarioAnswer resposta = servico.List(
                     x => (x.Nome!.ToUpper().StartsWith(obj.Nome!.ToUpper())
                     && x.Cd_UsuarioCriacao == (userSession!.Perfil == EnumPerfil.Administrador ? x.Cd_UsuarioCriacao : userSession.Nome)
                     && x.Documento!.StartsWith(obj.Documento ?? x.Documento)
                     && (x.Id_Empresa == (obj.Id_Empresa == 0 ? x.Id_Empresa : obj.Id_Empresa))
                     && (x.TipoContrato == (obj.TipoContrato == 0 ? x.TipoContrato : obj.TipoContrato))
                     && (obj.StatusFilter.HasValue ? x.Status == obj.StatusFilter > 0 : x.Status == x.Status)));

                var query = resposta.Funcionarios.ToPagedList(skip, 12);
                return PartialView("ListRecord", query);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/Funcionario/qrcode/{Id:int}")]
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
        [Route("/Funcionario/Cracha/{Id:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public PartialViewResult GetCracha(int Id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("PartilCracha", BadRequest("Usuario não autenticado"));

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                Cracha resposta = servico.GetCracha(Id);

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode($"https://{Request.Host}/Funcionario/qrcode/{Id}", QRCodeGenerator.ECCLevel.Q);
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

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
