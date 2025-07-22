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
    public class EmpresaController(IEmpresa<EmpresaAnswer> _servico, IObra<ObraAnswer> _servicoObra,
        IColaborador<ColaboradorAnswer> _servicoFuncionario, IAnexo<AnexoAnswer> _servicoAnexo,
        IDocumentoEmpresa<DocumentoEmpresaAnswer> _servicoDocumentoEmprea, ICliente<ClienteAnswer> _servicoCliente) : Controller
    {
        private readonly IEmpresa<EmpresaAnswer> servico = _servico;
        private readonly IObra<ObraAnswer> servicoObra = _servicoObra;
        private readonly IColaborador<ColaboradorAnswer> servicoFuncionario = _servicoFuncionario;
        private readonly IAnexo<AnexoAnswer> servicoAnexo = _servicoAnexo;
        private readonly IDocumentoEmpresa<DocumentoEmpresaAnswer> servicoDocumentoEmpresa = _servicoDocumentoEmprea;
        private readonly ICliente<ClienteAnswer> servicoCliente = _servicoCliente;

        [HttpGet]
        [Authorize(Roles = "Terceiro,Master, Administrador")]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest("Usuario não autenticado");

            var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);
            if (sessao == null)
                return RedirectToAction("Index", "Start");

            var empresa = servico.Get(g => g.Cd_Empresa == sessao.Cd_Empresa);
            return View(empresa);
        }

        [HttpGet]
        [Authorize(Roles = "Master, Administrador")]
        public ActionResult Create(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest("Usuario não autenticado");

            var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

            if(sessao ==  null)
                return BadRequest("sessão de usuario expirada");

            EmpresaAnswer? resposta = null;

            if (id > 0)
                resposta = servico.Get(s => s.Cd_Empresa == id);

            List<SelectListItem> selectListItems = 
                [.. 
                   servico.GetClientes(s => s.Status 
                   && s.Cd_Cliente == (sessao!.IsMaster ? sessao.Cd_Cliente_Id!: s.Cd_Cliente))
                   .Select(g => new SelectListItem { Value = g.Cd_Cliente.ToString(), Text = g.Nome })
                   .OrderBy(t => t.Text)
                ];
            ViewData["selectCliente"] = selectListItems;

            return id == 0 ? View() : View(resposta?.Empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Master, Administrador")]
        public IActionResult Create(Empresa empresa)
        {
            try
            {
                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);
                if (sessao == null)
                    return RedirectToAction("Index", "Start");

                ModelState.Remove("Cd_Empresa");

                if (ModelState.IsValid)
                {
                    if (empresa.Cd_Empresa > 0)
                    {
                        empresa.Cd_UsuarioAtualizacao = User.Identity?.Name ?? "ANONYMOUS";
                        empresa.Dt_Atualizacao = DateTime.Now;
                    }
                    else
                    {
                        empresa.Cnpj = Regex.Replace(empresa.Cnpj!, @"[^\d]", string.Empty);

                        empresa.Cd_UsuarioCriacao = User.Identity?.Name ?? "ANONYMOUS";
                        empresa.Id_UsuarioCriacao = sessao.Cd_Usuario;

                        var emp = servico.Get(s => s.Cnpj!.Equals(empresa.Cnpj));

                        if (emp.Success)
                            return BadRequest(EmpresaAnswer.DeFalha("CNPJ já cadastrado"));
                    }

                    empresa.Cnpj = Regex.Replace(empresa.Cnpj!, @"[^\d]", string.Empty);

                    EmpresaAnswer empresaAnswer = servico.Save(empresa, (empresa.Cd_Empresa == 0 ? EnumAcao.Criar : EnumAcao.Editar));

                    if (empresaAnswer.Success)
                    {
                        string codigos_documentoComplementarPadrão = "28,29,30,31,32,33,34,35,36,37,38,39"; // codigos de documentos padrão
                        servicoDocumentoEmpresa.Save(
                            new DocumentacaoEmpresa
                            {
                                Cd_Empresa_Id = empresa.Cd_Empresa,
                                Cd_Documento_Id = 2020,
                                Cd_Documentos_Complementares_Id = codigos_documentoComplementarPadrão,
                                Status = true
                            });
                    }

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
        [Authorize(Roles = "Master, Terceiro, Administrador")]
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
        [Authorize(Roles = "Master, Terceiro, Administrador")]
        public PartialViewResult Search(string empresa, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                Empresa? obj = JsonConvert.DeserializeObject<Empresa>(empresa);

                if (!string.IsNullOrWhiteSpace(obj.Cnpj))
                    obj.Cnpj = Regex.Replace(obj.Cnpj, @"[^0-9$]", string.Empty);


                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                EmpresaAnswer resposta = servico.List(
                     x => (x.Nome!.ToUpper().StartsWith(obj.Nome!.ToUpper())
                     //&& x.Cd_UsuarioCriacao == (sessao!.Perfil == EnumPerfil.Administrador ? x.Cd_UsuarioCriacao : sessao.Nome)
                     && (sessao.Perfil == EnumPerfil.Master ? x.Cd_Cliente_Id == sessao.Cd_Cliente_Id : sessao.Perfil == EnumPerfil.Administrador ? x.Cd_Cliente_Id > 0 : x.Cd_Empresa == sessao.Cd_Empresa )
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
        [Authorize(Roles = "Master, Terceiro, Administrador")]
        public PartialViewResult Obras(int id, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                ObraAnswer resposta = servicoObra.List(
                    x => x.Cd_Usuario_Criacao == (sessao.Perfil == EnumPerfil.Administrador ? x.Cd_Usuario_Criacao : sessao.Nome)
                    && x.Cd_Empresa_Id == id);

                var query = resposta.Obras.ToPagedList(skip, 14);
                return PartialView("ListObraRecord", query);
            }
            catch (Exception ex)
            {
                return PartialView("ListRecord", BadRequest($"Ocorreu um erro na execução, ERRO:{ex.Message}"));
            }
        }

        [HttpGet]
        [Route("/Empresa/GetObras/")]
        [Authorize(Roles = "Master, Terceiro, Administrador")]
        public IActionResult GetObrasJson(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Start");

                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                if (sessao != null)
                {
                    ObraAnswer resposta = servicoObra.List(x => x.Cd_Empresa_Id == id && x.Status);

                    List<SelectListItem> listEmp = [.. resposta.Obras.Select(s => new SelectListItem(s.Nome, s.Cd_Empresa_Id.ToString()))];
                    return new JsonResult(resposta);
                }

                return RedirectToAction("Index", "Start");
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("/Empresa/AddObra")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Master, Terceiro, Administrador")]
        public IActionResult AddObras(string comandoObra)
        {
            try
            {
                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                if (sessao != null)
                {
                    Obra obj = JsonConvert.DeserializeObject<Obra>(comandoObra) ?? new();

                    if (obj.Cd_Obra == 0)
                    {
                        obj.Cd_Usuario_Criacao = sessao.Nome;
                        obj.Id_UsuarioCriacao = sessao.Cd_Usuario;
                    }

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
        [Authorize(Roles = "Master, Terceiro, Administrador")]
        public PartialViewResult? SearchFuncionario(int id_Empresa, int id_Obra, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListColaboradorRecord", BadRequest("Usuario não autenticado"));

                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                ColaboradorAnswer resposta = servicoFuncionario.List(
                     x => x.Status && x.Id_Empresa == id_Empresa);
                var query = resposta.Colaboradores;
                var filter = query.Select(s => new Colaborador
                {
                    Cd_Funcionario = s.Cd_Funcionario,
                    Nome = s.Nome,
                    Documento = s.Documento,
                    Telefone = s.Telefone,
                    VinculoObras = s.VinculoObras?.Where(g => g.Cd_Obra_Id == id_Obra).ToList()
                }).ToPagedList(skip, 15);
                return PartialView("ListColaboradorRecord", filter);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("/Empresa/Vincular")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Master, Terceiro, Administrador")]
        public IActionResult VincularObras(string comandoVincularObra)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Start");

                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                if (sessao != null)
                {
                    ColaboradorVinculoObra obj = JsonConvert.DeserializeObject<ColaboradorVinculoObra>(comandoVincularObra) ?? new();
                    obj.Cd_UsuarioCriacao = sessao.Nome;
                    obj.Cd_Cliente = sessao.Cliente!.Cd_Cliente;
                    obj.Id_UsuarioCriacao = sessao.Cd_Usuario;

                    int ret = servico.Vincular(obj);
                    var retorno = new { success = ret > 0, message = "item registrado com sucesso" };
                    return new JsonResult(retorno);
                }

                return BadRequest(ObraAnswer.DeErro("não foi poissivel inserir a Obra."));
            }
            catch (Exception ex)
            {
                return BadRequest(ObraAnswer.DeErro($"Ocorreu um erro na execução, ERRO: {ex.Message}"));
            }

        }

        [HttpDelete("/empresa/remove/{id:int}")]
        [Authorize(Roles = "Administrador, Master")]
        public IActionResult RemoveEmpresa(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Start");

                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);
                if (sessao != null)
                {

                    servicoAnexo.List(x => x.Cd_Empresa_Id == id, false).Anexos.ForEach(x => servicoAnexo.Remove(x));
                    int total = servicoFuncionario.List(x => x.Id_Empresa == id).Colaboradores.Count;

                    EmpresaAnswer resposta = servico.Remove(id);

                    if (resposta.Success)
                    {
                        sessao.Cliente = servicoCliente.Get(s => s.Cd_Cliente == sessao.Cliente.Cd_Cliente).Cliente;

                        Util.Utility.Cliente.PlanoVidasAtivadas = (sessao.Cliente.PlanoVidasAtivadas - total);
                        servicoCliente.UpdateVidas(Util.Utility.Cliente);
                    }

                    return new JsonResult(resposta);
                }
                return BadRequest(EmpresaAnswer.DeErro("Usuário com a sessão expirida."));
            }
            catch (Exception ex)
            {
                return BadRequest(EmpresaAnswer.DeErro($"Ocorreu um erro na execução, ERRO: {ex.Message}"));
            }

        }
    }
}
