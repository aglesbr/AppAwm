using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using AppAwm.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using X.PagedList.Extensions;

namespace AppAwm.Controllers
{
    public class OperacaoController(
                IEmpresa<EmpresaAnswer> _servicoEmpresa,
                IAnexo<AnexoAnswer> _servicoAnexo,
                IUsuario<UsuarioAnswer> _servicoUsuario,
                IColaborador<ColaboradorAnswer> _servicoFuncionario,
                ICargo<CargoAnswer> _servicoCargo,
                IDocumentacaoComplementar<DocumentacaoComplementarAnswer> _servicoDocumentacaoComplementar) : Controller
    {
        private readonly IEmpresa<EmpresaAnswer> servicoEmpresa = _servicoEmpresa;
        private readonly IAnexo<AnexoAnswer> servicoAnexo = _servicoAnexo;
        private readonly IUsuario<UsuarioAnswer> servicoUsuario = _servicoUsuario;
        private readonly IColaborador<ColaboradorAnswer> servicoFuncionario = _servicoFuncionario;
        private readonly ICargo<CargoAnswer> servicoCargo = _servicoCargo;
        private readonly IDocumentacaoComplementar<DocumentacaoComplementarAnswer> servicoDocumentacaoComplementar = _servicoDocumentacaoComplementar;

        [Authorize(Roles = "Administrador, Analista")]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Start");

            EmpresaAnswer empresaAnswer = servicoEmpresa.List(s => s.Status);
            List<SelectListItem> selectListItems = [.. empresaAnswer.Empresas.Select(s => new SelectListItem { Text = s.Nome, Value = s.Cd_Empresa.ToString() }).OrderBy(o => o.Text)];

            ViewData["selectsEmpresa"] = selectListItems;
            return View();
        }

        [HttpGet]
        [Route("/Operacao/Search/{skip:int}")]
        [Authorize(Roles = "Administrador")]
        public PartialViewResult? Search(string cd_empresa, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                if (userSession == null)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                if (string.IsNullOrWhiteSpace(cd_empresa))
                    cd_empresa = "0";


                int codigoEmpresa = Convert.ToInt32(cd_empresa);

                AnexoAnswer anexoAnswer = servicoAnexo.List(
                     x => (x.Cd_Empresa_Id == (codigoEmpresa == 0 ? x.Cd_Empresa_Id : codigoEmpresa))
                     && (x.Status != EnumStatusDocs.None)
                     );


                var query = anexoAnswer.Anexos
                    .OrderByDescending(ob => ob.Dt_Criacao)
                    .DistinctBy(d => d.TipoAnexo)
                    .Select(s =>
                    new Anexo
                    {
                        Cd_Anexo = s.Cd_Anexo,
                        Nome = s.Nome,
                        Descricao = s.Descricao,
                        TipoAnexo = s.TipoAnexo,
                        Status = s.Status,
                        Dt_Criacao = s.Dt_Criacao,
                        Cd_UsuarioAnalista = s.Cd_UsuarioAnalista
                    })

                    .Where(w => w!.Status is EnumStatusDocs.Rejeitado or EnumStatusDocs.Resalva)
                    .ToList();

                var queryGroup = query.GroupBy(gb => gb.TipoAnexo).Select(ss => ss.FirstOrDefault())

                    .ToPagedList(skip, 12);

                return PartialView("ListRecord", queryGroup);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/Operacao/Search")]
        public IActionResult? SearchMq(int idAnexo, int idEmpresa, EnumStatusDocs status = 0, int tipoAnexo = 0, int origem = 0, int pageNumber = 0)
        {
            int pageSize = 300;

            try
            {
                AnexoAnswer? resposta = null;

                if (idAnexo > 0)
                {
                    resposta = servicoAnexo.List(x => x.TipoAnexo > 0 && x.Status > 0 && x.Cd_Anexo == idAnexo);
                }
                else
                {
                    resposta = servicoAnexo.List(
                         x => x.TipoAnexo > 0 && x.Status > 0
                        && x.Cd_Anexo == (idAnexo == 0 ? x.Cd_Anexo : idAnexo)
                        && x.Status == (status == EnumStatusDocs.None ? x.Status : status)
                        && x.TipoAnexo == (tipoAnexo == 0 ? x.TipoAnexo : tipoAnexo)
                        && x.Cd_Empresa_Id == (idEmpresa == 0 ? x.Cd_Empresa_Id : idEmpresa)
                        && (origem == 1 ? x.Cd_Funcionario_Id != null : x.Cd_Funcionario_Id == null));
                }

                int countRegistro = resposta.Anexos.Count;

                var query = resposta.Anexos.Select(s =>
                new
                {
                    s.Cd_Anexo,
                    s.Cd_Funcionario_Id,
                    s.Cd_Empresa_Id,
                    s.Nome,
                    s.Descricao,
                    s.TipoAnexo,
                    s.Status,
                    s.Dt_Criacao,
                    s.Dt_Validade_Documento,
                    s.Cd_UsuarioCriacao,
                    s.MotivoResalva,
                    s.MotivoRejeicao,
                    TotalRegistro = countRegistro,
                    TotalPaginas = ((int)Math.Ceiling(decimal.Parse(countRegistro.ToString()) / decimal.Parse(pageSize.ToString())))
                })
                .OrderByDescending(n => n.Dt_Criacao)
                //.GroupBy(gb => gb.Status).Select(ss => ss.FirstOrDefault())
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToList();

                return new JsonResult(query);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/Operacao/GetEmpesa")]
        public IActionResult? GetEmpresa()
        {
            try
            {
                EmpresaAnswer empresaAnswer = servicoEmpresa.List(s => s.Status);
                var query = empresaAnswer.Empresas.OrderBy(o => o.Nome)
                    .Select(s => new { Id = s.Cd_Empresa, s.Nome });

                return new JsonResult(query);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/Operacao/GetAnexo")]
        public IActionResult? GetAnexo(int id)
        {
            try
            {
                AnexoAnswer resposta = servicoAnexo.List(x => x.Status > 0 && (x.Cd_Anexo == (id == 0 ? x.Cd_Anexo : id)), true);

                if (resposta.Success)
                {
                    var obj = resposta.Anexos.FirstOrDefault()!;
                    obj.Status = obj.Status == EnumStatusDocs.Enviado ? EnumStatusDocs.EmAnalise : obj.Status;

                    if (obj.Cd_Funcionario_Id != null)
                    {
                        ColaboradorAnswer funcionarioAnswer = servicoFuncionario.Get(f => f.Cd_Funcionario == obj.Cd_Funcionario_Id, new Usuario { Perfil = EnumPerfil.Administrador });

                        if (resposta.Success)
                            obj.Colaborador = new Colaborador { Nome = funcionarioAnswer.Colaborador.Nome };
                    }
                    else
                    {
                        EmpresaAnswer empresaAnswer = servicoEmpresa.Get(emp => emp.Cd_Empresa == obj.Cd_Empresa_Id && emp.Status);
                        if (empresaAnswer.Success)
                            obj.Empresa = new Empresa { Nome = empresaAnswer.Empresa.Nome, NomeFantasia = empresaAnswer.Empresa.NomeFantasia };
                    }
                    return new JsonResult(resposta.Success ? obj : new Anexo { Arquivo = [] });
                }

                return new JsonResult(new Anexo { Arquivo = [] });
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [Route("/Operacao/UpdateStatus")]
        public IActionResult? UpdateStatus(int id, EnumStatusDocs enumStatus, string usuarioAnalista, string? motivo)
        {
            try
            {
                Anexo obj = new() { Arquivo = [] };
                AnexoAnswer resposta = servicoAnexo.UpdateStatus(id, enumStatus, usuarioAnalista, string.IsNullOrWhiteSpace(motivo) ? null : motivo);

                if (enumStatus == EnumStatusDocs.Aprovado)
                {
                    if (resposta.Success)
                    {
                        resposta = servicoAnexo.List(s => s.Cd_Anexo == id);
                        obj = resposta.Success ? resposta.Anexos.FirstOrDefault()! : new() { Arquivo = [] };
                    }

                    return new JsonResult(resposta.Success ? obj : new Anexo { Arquivo = [] });
                }

                return new JsonResult(resposta.Success);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/Operacao/GetUserInfAll")]
        public IActionResult? GetUser(string nameUser, int idFunc, int idEmp)
        {
            try
            {
                UsuarioAnswer usuarioAnswer = servicoUsuario.Get(s => s.Status && s.Nome == nameUser, EnumAcao.Nenhum);
                Colaborador? funcionarioAnswer = servicoFuncionario.List(f => f.Status && f.Cd_Funcionario == idFunc).Colaboradores.FirstOrDefault() ?? new Colaborador();
                EmpresaAnswer empresaAnswer = servicoEmpresa.Get(g => g.Status && g.Cd_Empresa == idEmp);

                if (usuarioAnswer.Success)
                {
                    return new JsonResult(
                        new
                        {
                            Colaborador = new
                            {
                                funcionarioAnswer.Cd_Funcionario,
                                funcionarioAnswer.Nome,
                                funcionarioAnswer.Documento,
                                funcionarioAnswer.Telefone
                            },
                            Usuario = new
                            {
                                usuarioAnswer.Usuario.Cd_Usuario,
                                usuarioAnswer.Usuario.Nome,
                                usuarioAnswer.Usuario.Email,
                                usuarioAnswer.Usuario.Documento,
                                usuarioAnswer.Usuario.Telefone
                            },
                            Empresa = new
                            {
                                empresaAnswer.Empresa.Cd_Empresa,
                                empresaAnswer.Empresa.Nome,
                                empresaAnswer.Empresa.Cnpj,
                                empresaAnswer.Empresa.Telefone,
                                empresaAnswer.Empresa.Email,
                            }
                        });
                }

                return new JsonResult(new Usuario());
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/Operacao/GetColaborador/{id:int}")]
        public IActionResult GetFuncionario(int id = 0)
        {
            ColaboradorAnswer funcionarioAnswer = servicoFuncionario.Get(p => p.Cd_Funcionario == id && p.Status, new Usuario { Perfil = EnumPerfil.None });

            if (funcionarioAnswer.Success)
            {
                EmpresaAnswer empresaAnswer = servicoEmpresa.Get(g => g.Cd_Empresa == funcionarioAnswer.Colaborador.Id_Empresa);

                if (empresaAnswer.Success)
                {
                    Empresa empresa = new Empresa { Cd_Empresa = empresaAnswer.Empresa.Cd_Empresa, Nome = empresaAnswer.Empresa.Nome };
                    funcionarioAnswer.Colaborador.Empresa = empresa;
                }

                if (funcionarioAnswer.Success && empresaAnswer.Success)
                    return Ok(funcionarioAnswer.Colaborador);
            }

            return BadRequest(funcionarioAnswer.Message);
        }

        [HttpGet]
        [Route("/Operacao/GetCargos")]
        public IActionResult GetCargosParcialPorNomeCargo(string? nome = null)
        {
            CargoAnswer cargoAnswer = servicoCargo.List(s => s.Nome.StartsWith(nome ?? s.Nome));

            return Ok(cargoAnswer);
        }

        [HttpPut]
        [Route("/Operacao/UpdateStatusDocumentoCargo")]
        public IActionResult PutUpdateStatusDocumento(int id, string id_documento, bool isAtivo)
        {
            CargoAnswer cargoAnswer = servicoCargo.UpdateStatus(id, id_documento, isAtivo);

            return Ok(cargoAnswer);
        }

        [HttpGet]
        [Route("/Operacao/GetDocumentoVsCargo")]
        public IActionResult GetDocumentoVsCargo(int id)
        {
            List<DocumentacaoCargo> lst = servicoCargo.GetDocumentoVsCargo(s => s.Cd_Cargo_Id == id);
            return Ok(lst);
        }

        [HttpGet]
        [Route("/Operacao/GetDocumentoComplementar")]
        public IActionResult DocumentoComplementar()
        {
            DocumentacaoComplementarAnswer documentacao = servicoDocumentacaoComplementar.Get(s => s.Status);
            return Ok(documentacao);
        }
    }
}
