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

    public class FuncoesController(
            ICargo<CargoAnswer> _servico, 
            IDocumentacaoComplementar<DocumentacaoComplementarAnswer> documentacao,
            IEmpresa<EmpresaAnswer> empresa,
            IDocumentoCargo<DocumentoCargoAnswer> documentoCargo) : Controller
    {
        private readonly ICargo<CargoAnswer> servico = _servico;
        private readonly IDocumentacaoComplementar<DocumentacaoComplementarAnswer> servicoDocumentoComplementar = documentacao;
        private readonly IEmpresa<EmpresaAnswer> servicoEmpresa = empresa;
        private readonly IDocumentoCargo<DocumentoCargoAnswer> documentoCargoServico = documentoCargo;

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

        // GET: FuncoesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FuncoesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FuncoesController/Create
        [HttpPost]
        [Route("Funcoes/vincular-documento-cargo")]
        [Authorize(Roles = "Administrador")]
        public ActionResult VincularDesvicular(ComandoDocumentoCargo comandoDocumentoCargo)
        {
            try
            {
                DocumentacaoCargo documentacaoCargo = new() 
                {
                    Cd_Cargo_Id = comandoDocumentoCargo.Cd_Cargo_Id,
                    Cd_Documento_Id = comandoDocumentoCargo.Cd_Documento_Id,
                    Cd_Empresa_Id = comandoDocumentoCargo.Cd_Empresa
                };

                DocumentoCargoAnswer resposa = documentoCargoServico.Save(documentacaoCargo, comandoDocumentoCargo.Vinculado);
                return Ok(comandoDocumentoCargo);
            }
            catch
            {
                return BadRequest(comandoDocumentoCargo);
            }
        }

        [HttpGet("/funcoes/Search/{skip:int}")]
        public ActionResult GetCargosAutoComplete(string funcoes, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                Cargo obj = JsonConvert.DeserializeObject<Cargo>(funcoes) ?? new();

                CargoAnswer resposta = servico.List(c => c.Nome.ToUpper().StartsWith(obj.Nome.ToUpper()));

                var query = resposta.Cargos.ToPagedList(skip, 13);

                return PartialView("ListRecord", query);
            }
            catch (Exception ex)
            {
                return BadRequest(CargoAnswer.DeErro($"Ocorreu um erro: {ex.Message}"));
            }
        }


        [HttpGet]
        [Route("/funcoes/typeDocument/{skip:int}")]
        [Authorize(Roles = "Administrador")]
        public PartialViewResult GetFuncoesTipoDocumento(int id, int origem,int? cd_empresa, int skip)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                return PartialView("ListRecordTypeDocument", BadRequest(DocumentacaoComplementarAnswer.DeErro("Usuario não esta autenticado")));

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                var documentoComplementar = servicoDocumentoComplementar.GetDocumentoCargo(id, cd_empresa, origem);

                return PartialView("ListRecordTypeDocument", documentoComplementar.Success ? documentoComplementar.DocumentacaoComplementares.ToPagedList(skip,11) : documentoComplementar.Message);
            }
            catch (Exception ex)
            {
                return PartialView("ListRecordTypeDocument", BadRequest(DocumentacaoComplementarAnswer.DeErro(ex.Message)));
            }

        }

        // GET: FuncoesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FuncoesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
