using AppAwm.Comando;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using X.PagedList.Extensions;

namespace AppAwm.Controllers
{
    [Authorize]
    public class EmpresaController(IEmpresa<EmpresaAnswer> _servico, IObra<ObraAnswer> _servicoObra, IFuncionario<FuncionarioAnswer> _servicoFuncionario) : Controller
    {
        private readonly IEmpresa<EmpresaAnswer> servico = _servico;
        private readonly IObra<ObraAnswer> servicoObra = _servicoObra;
        private readonly IFuncionario<FuncionarioAnswer> servicoFuncionario = _servicoFuncionario;

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create(int id)
        {
            var resposta = servico.Get(s => s.Cd_Empresa == id, EnumAcao.Editar);
            return id == 0 ? View() : View(resposta.Empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public IActionResult Create(Empresa empresa)
        {
            try
            {
                ModelState.Remove("Cd_Empresa");
                ModelState.Remove("Endereco.Cd_Endereco");
                ModelState.Remove("Endereco.Cd_Empresa");
                if (ModelState.IsValid)
                {
                    if (empresa.Cd_Empresa > 0)
                    {
                        empresa.Cd_UsuarioAtualizacao = User.Identity?.Name ?? "ANONYMOUS";
                        empresa.Dt_Atualizacao = DateTime.Now;
                        empresa.Endereco.Dt_Atualizacao = DateTime.Now;
                        empresa.Endereco.Cd_UsuarioAtualizacao = User.Identity?.Name ?? "ANONYMOUS";
                    }
                    else
                    {
                        empresa.Cd_UsuarioCriacao = User.Identity?.Name ?? "ANONYMOUS";
                        empresa.Endereco.Cd_UsuarioCriacao = User.Identity?.Name ?? "ANONYMOUS";

                        var emp = servico.Get(s => s.Cnpj!.Equals(empresa.Cnpj), EnumAcao.Criar);

                        if (emp.Success)
                            return BadRequest(EmpresaAnswer.DeFalha("CNPJ já cadastrado"));
                    }

                    empresa.Cnpj = Regex.Replace(empresa.Cnpj!, @"[^\d]", string.Empty);

                    EmpresaAnswer empresaAnswer = servico.Save(empresa, (empresa.Cd_Empresa == 0 ? EnumAcao.Criar : EnumAcao.Editar));

                    return empresaAnswer.Success ? Ok(empresaAnswer) : BadRequest(empresaAnswer);

                }
                string[] messages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(EmpresaAnswer.DeErro(messages));
            }
            catch (Exception ex)
            {
                return BadRequest(EmpresaAnswer.DeErro(ex.Message));
            }
        }


        [HttpPost]
        [Route("Empresa/search/cnpj")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public IActionResult? ConsultaEmpresa(string cnpj)
        {
            try
            {
                cnpj = Regex.Replace(cnpj, @"[^0-9$]", string.Empty);
                var resposta = servico.GetCnpj(cnpj).Result;
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(EmpresaAnswer.DeErro(ex.Message));
            }
        }


        [HttpGet]
        [Route("/Empresa/Search/{skip:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public PartialViewResult Search(string empresa, int skip = 1)
        {
            try
            {
                Empresa? obj = JsonConvert.DeserializeObject<Empresa>(empresa);

                if (!string.IsNullOrWhiteSpace(obj.Cnpj))
                    obj.Cnpj = Regex.Replace(obj.Cnpj, @"[^0-9$]", string.Empty);


                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                EmpresaAnswer resposta = servico.List(
                     x => (x.Nome!.ToUpper().StartsWith(obj.Nome!.ToUpper())
                     && x.Cd_UsuarioCriacao == (sessao.Perfil == EnumPerfil.Administrador ? x.Cd_UsuarioCriacao : sessao.Nome)
                     && (string.IsNullOrWhiteSpace(obj.NomeFantasia) ? x.NomeFantasia == x.NomeFantasia : x.NomeFantasia.ToUpper().StartsWith(obj.NomeFantasia.ToUpper()))
                     && x.Cnpj!.StartsWith(obj.Cnpj ?? x.Cnpj)
                     && (obj.StatusFilter.HasValue ? x.Status == obj.StatusFilter > 0 : x.Status == x.Status)));

                var query = resposta.Empresas.ToPagedList(skip, 12);
                return PartialView("ListRecord", query);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("/Empresa/Obras/{skip:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public PartialViewResult Obras(int id, int skip = 1)
        {
            try
            {
                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                ObraAnswer resposta = servicoObra.List(
                    x => x.Cd_Usuario_Criacao == (sessao.Perfil == EnumPerfil.Administrador ? x.Cd_Usuario_Criacao : sessao.Nome)
                    && x.Cd_Empresa_Id == id);

                var query = resposta.Obras.ToPagedList(skip, 6);
                return PartialView("ListObraRecord", query);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("/Empresa/GetObras/")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public IActionResult GetObrasJson(int id)
        {
            try
            {
                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                ObraAnswer resposta = servicoObra.List(
                    x => x.Cd_Usuario_Criacao == (sessao.Perfil == EnumPerfil.Administrador ? x.Cd_Usuario_Criacao : sessao.Nome)
                    && x.Cd_Empresa_Id == id && x.Status);

                List<SelectListItem> listEmp = [..resposta.Obras.Select(s => new SelectListItem(s.Nome, s.Cd_Empresa_Id.ToString()))];
                return new JsonResult(resposta);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("/Empresa/AddObra")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public IActionResult AddObras(string comandoObra)
        {
            try
            {
                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                if (sessao != null)
                {
                    Obra obj = JsonConvert.DeserializeObject<Obra>(comandoObra) ?? new();
                    obj.Cd_Usuario_Criacao = sessao.Nome;

                    if (!obj.Status)
                    {
                        ObraAnswer answerCheck = servicoObra.SeVinculado(v => v.Vinculado && v.Cd_Empresa_Id == obj.Cd_Empresa_Id && v.Cd_Obra_Id == obj.Cd_Obra);
                        
                        if (!answerCheck.Success)
                        {
                            return BadRequest(answerCheck);
                        }
                    }

                    ObraAnswer obraAnswer = servicoObra.Save(obj);

                    return obraAnswer.Success ? Ok(obraAnswer) : BadRequest(obraAnswer);
                }

                return BadRequest(ObraAnswer.DeErro("não foi poissivel inserir a Obra."));
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("/Empresas/FuncionarioByObra/{skip:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public PartialViewResult? SearchFuncionario(int id_Empresa, int id_Obra, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListColaboradorRecord", BadRequest("Usuario não autenticado"));

                FuncionarioAnswer resposta = servicoFuncionario.List(
                     x => x.Status && x.Id_Empresa == id_Empresa);
                var query = resposta.Funcionarios;
                var filter = query.Select(s => new Funcionario { 
                    Cd_Funcionario = s.Cd_Funcionario,
                    Nome = s.Nome, 
                    Documento = s.Documento,
                    Telefone = s.Telefone,
                    VinculoObras = s.VinculoObras.Where(g => g.Cd_Obra_Id == id_Obra).ToList() }).ToPagedList(skip, 12);
                return PartialView("ListColaboradorRecord", filter);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("/Empresa/Vincular")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public IActionResult VincularObras(string comandoVincularObra)
        {
            try
            {
                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                if (sessao != null)
                {
                    FuncionarioVinculoObra obj = JsonConvert.DeserializeObject<FuncionarioVinculoObra>(comandoVincularObra) ?? new();
                    obj.Cd_UsuarioCriacao = sessao.Nome;
                    int ret = servico.Vincular(obj);
                    var retorno = new { success = ret > 0, message = "item registrado com sucesso" };
                    return new JsonResult(retorno);
                }

                return BadRequest(ObraAnswer.DeErro("não foi poissivel inserir a Obra."));
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
