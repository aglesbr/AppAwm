using AppAwm.Comando;
using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using X.PagedList.Extensions;

namespace AppAwm.Controllers
{

    public class DocumentacaoController(
            ICargo<CargoAnswer> _servico,
            IDocumentacaoComplementar<DocumentacaoComplementarAnswer> documentacao,
            IEmpresa<EmpresaAnswer> empresa,
            IDocumentoCargo<DocumentoCargoAnswer> documentoCargo,
            IDocumentoEmpresa<DocumentoEmpresaAnswer> documentoEmpresa) : Controller
    {
        private readonly ICargo<CargoAnswer> servico = _servico;
        private readonly IDocumentacaoComplementar<DocumentacaoComplementarAnswer> servicoDocumentoComplementar = documentacao;
        private readonly IEmpresa<EmpresaAnswer> servicoEmpresa = empresa;
        private readonly IDocumentoCargo<DocumentoCargoAnswer> documentoCargoServico = documentoCargo;
        private readonly IDocumentoEmpresa<DocumentoEmpresaAnswer> documentoEmpresaServico = documentoEmpresa;

        // GET: FuncoesController
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("index", "Start");

            List<SelectListItem> selectListItems = [.. servicoEmpresa.List(s => s.Status).Empresas
                .Select(s => new SelectListItem { Text = s.Nome, Value = s.Cd_Empresa.ToString() }).OrderBy(o => o.Text)];
            ViewData["selectsEmpresa"] = selectListItems;

            return View();
        }

        // POST: FuncoesController/Create
        [HttpPost]
        [Route("Documentacao/vincular-documento")]
        [Authorize(Roles = "Administrador")]
        public ActionResult VincularDesvicular(ComandoDocumento comandoTipoDocumento)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));


                if (comandoTipoDocumento.Origem == 1)
                {
                    DocumentacaoCargo documentacaoCargo = new()
                    {
                        Cd_Cargo_Id = comandoTipoDocumento.Cd_Cargo_Id,
                        Cd_Documento_Id = comandoTipoDocumento.Cd_Documento_Id,
                        Cd_Empresa_Id = comandoTipoDocumento.Cd_Empresa_Id == 0 ? null : comandoTipoDocumento.Cd_Empresa_Id
                    };

                    DocumentoCargoAnswer resposa = documentoCargoServico.Save(documentacaoCargo, comandoTipoDocumento.Vinculado);
                    return Ok(comandoTipoDocumento);
                }
                else
                {
                    DocumentacaoEmpresa documentacaoEmpresa = new()
                    {
                        Cd_Documentos_Complementares_Id = comandoTipoDocumento.Cd_Documento_Complementar.ToString(),
                        Cd_Documento_Id = comandoTipoDocumento.Cd_Documento_Id,
                        Cd_Empresa_Id = comandoTipoDocumento.Cd_Empresa_Id,
                        Status = comandoTipoDocumento.Status
                    };

                    DocumentoEmpresaAnswer resposa = documentoEmpresaServico.Save(documentacaoEmpresa, comandoTipoDocumento.Vinculado);
                    return Ok(comandoTipoDocumento);
                }
            }
            catch
            {
                return BadRequest(comandoTipoDocumento);
            }
        }

        [HttpGet("/documentacao/Search/{skip:int}")]
        public ActionResult GetTipodocumentacao(string documentacao, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                CargoAnswer respostaCargo = null;
                EmpresaAnswer respostaEmpresa = null;

                Documento_Empresa_Cargo obj = JsonConvert.DeserializeObject<Documento_Empresa_Cargo>(documentacao) ?? new();

                if (obj.Origem == 1)
                {
                    respostaCargo = servico.List(c => c.Nome.ToUpper().StartsWith(obj.Nome.ToUpper()));
                    var query = respostaCargo.Cargos
                        .Select(s => new Documento_Empresa_Cargo { Cd = s.Cd_Cargo, Nome = s.Nome, Status = s.Status, Origem = 1 })
                        .ToPagedList(skip, 14);
                    return PartialView("ListRecord", query);
                }
                else
                {
                    respostaEmpresa = servicoEmpresa.List(s => s.Nome.ToUpper().StartsWith(obj.Nome.ToUpper()));
                    var query = respostaEmpresa.Empresas
                        .Select(s => new Documento_Empresa_Cargo { Cd = s.Cd_Empresa, Nome = s.Nome, Status = s.Status, Origem = 2 })
                        .ToPagedList(skip, 14);
                    return PartialView("ListRecord", query);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(CargoAnswer.DeErro($"Ocorreu um erro: {ex.Message}"));
            }
        }


        [HttpGet]
        [Route("/documentacao/typeDocument/{skip:int}")]
        [Authorize(Roles = "Administrador")]
        public PartialViewResult GetTipoDocumento(int id, int cd_empresa_id, int origem, int skip)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecordTypeDocument", BadRequest(DocumentacaoComplementarAnswer.DeErro("Usuario não esta autenticado")));

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                var documentoComplementar = servicoDocumentoComplementar.GetTipoDocumento(id, cd_empresa_id, origem);

                return PartialView("ListRecordTypeDocument", documentoComplementar.Success ? documentoComplementar.DocumentacaoComplementares.ToPagedList(skip, 15) : documentoComplementar.Message);
            }
            catch (Exception ex)
            {
                return PartialView("ListRecordTypeDocument", BadRequest(DocumentacaoComplementarAnswer.DeErro(ex.Message)));
            }

        }
    }
}
