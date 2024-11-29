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
                IFuncionario<FuncionarioAnswer> _servicoFuncionario,
                ITreinamento<TreinamentoAnswer> _servicoTreinamento,
                ICargo<CargoAnswer> _servicoCargo,
                IDocumentacaoComplementar<DocumentacaoComplementarAnswer> _servicoDocumentacaoComplementar) : Controller
    {
        private readonly IEmpresa<EmpresaAnswer> servicoEmpresa = _servicoEmpresa;
        private readonly IAnexo<AnexoAnswer> servicoAnexo = _servicoAnexo;
        private readonly IUsuario<UsuarioAnswer> servicoUsuario = _servicoUsuario;
        private readonly IFuncionario<FuncionarioAnswer> servicoFuncionario = _servicoFuncionario;
        private readonly ITreinamento<TreinamentoAnswer> servicoTreinamento = _servicoTreinamento;
        private readonly ICargo<CargoAnswer> servicoCargo = _servicoCargo;
        private readonly IDocumentacaoComplementar<DocumentacaoComplementarAnswer> servicoDocumentacaoComplementar = _servicoDocumentacaoComplementar;

        [Authorize(Roles = "Administrador")]
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
        //[Authorize(Roles = "Administrador")]
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

                AnexoAnswer resposta = servicoAnexo.List(
                     x => (x.Cd_UsuarioAnalista == userSession.Nome || x.Cd_UsuarioAnalista == (userSession.Perfil == EnumPerfil.Administrador ? x.Cd_UsuarioAnalista : null))
                     && (x.Cd_Empresa_Id == (codigoEmpresa == 0 ? x.Cd_Empresa_Id : codigoEmpresa))
                     && x.Status != null);
                var query = resposta.Anexos.Select(s =>
                new Anexo
                {
                    Cd_Anexo = s.Cd_Anexo,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    TipoAnexo = s.TipoAnexo,
                    Status = s.Status,
                    Dt_Criacao = s.Dt_Criacao,
                    Cd_UsuarioAnalista = s.Cd_UsuarioAnalista
                }).ToPagedList(skip, 12);

                return PartialView("ListRecord", query);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/Operacao/Search")]
        public IActionResult? SearchMq(int id, EnumStatusDocs status = 0, string tipo = "0", int pageNumber = 0)
        {
            int pageSize = 16;

            try
            {
                AnexoAnswer resposta = servicoAnexo.List(
                     x => x.Status != null && x.Status == (status == EnumStatusDocs.None ? x.Status : status)
                   && (x.Cd_Empresa_Id == (id == 0 ? x.Cd_Empresa_Id : id))
                   && (x.TipoAnexo == (tipo == "0" ? x.TipoAnexo : tipo)));

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
                    s.Cd_UsuarioCriacao,
                    TotalRegistro = countRegistro,
                    TotalPaginas = ((int)Math.Ceiling(decimal.Parse(countRegistro.ToString()) / decimal.Parse(pageSize.ToString())))
                })
                .OrderBy(n => n.Nome)
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
                AnexoAnswer resposta = servicoAnexo.List(
                     x => x.Status > 0
                   && (x.Cd_Anexo == (id == 0 ? x.Cd_Anexo : id)));

                if (resposta.Success)
                {
                    var obj = resposta.Anexos.FirstOrDefault()!;
                    obj.Status = obj.Status == EnumStatusDocs.Enviado ? EnumStatusDocs.EmAnalise : obj.Status;

                    FuncionarioAnswer funcionarioAnswer = servicoFuncionario.Get(f => f.Cd_Funcionario == obj.Cd_Funcionario_Id, new Usuario { Perfil = EnumPerfil.Administrador });

                    if (resposta.Success)
                        obj.Funcionario = new Funcionario { Nome = funcionarioAnswer.Funcionario.Nome };

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
                Funcionario? funcionarioAnswer = servicoFuncionario.List(f => f.Status && f.Cd_Funcionario == idFunc).Funcionarios.FirstOrDefault() ?? new Funcionario();
                EmpresaAnswer empresaAnswer = servicoEmpresa.Get(g => g.Status && g.Cd_Empresa == idEmp, EnumAcao.Nenhum);

                if (usuarioAnswer.Success)
                {
                    return new JsonResult(
                        new
                        {
                            Funcionario = new
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
        [Route("/Operacao/Monitor")]
        public IActionResult? ValidaDatasDocumenot()
        {
            try
            {
                bool hasAnexo = false;

                string mensagem = string.Empty;
                mensagem += "<html><body><h3><center>HDdoc - AVISO DE VALIDADE DE DOCUMENTO</center><hr/></h3><br/><p>Olá {0}<br/></p>";
                mensagem += "<p>O sistema identificou documentos com o prazo de validade proximo do vencimento.</p>";
                mensagem += "<div style='border:1px solid black; padding:10px; border-radius: 5px;'><br>DOCUMENTO:<br><ul>{1}</ul><br>FUNCIONARIO: {2}<br>EMPRESA: {3}";
                mensagem += "</div><p>Resposta automático!<br/>Favor não responder este e-mail!</p></body></html>";

                var execucao = servicoAnexo.GetLastHistoricoExecucao() ?? new HistoricoExecucao { Dt_Execucao = DateTime.Now.Date };

                var date = (execucao?.Dt_Execucao.Month == DateTime.Now.Date.Month );

                if (date)
                {
                    servicoAnexo.InsereProximaExecucao(new HistoricoExecucao { Dt_Execucao = execucao!.Dt_Execucao.AddMonths(1) });

                    var lista = servicoFuncionario.List(x => x.Status && x.Anexos!.Count > 0);

                    var users = servicoUsuario.List(y => y.Status);
                    string docs = string.Empty;

                    if (lista.Success)
                    {
                        lista.Funcionarios.ForEach(x => { x.Empresa!.Complemento = null; x.Foto = null; });

                        foreach (var item in lista.Funcionarios)
                        {
                            var vencimentos = item.Anexos!.Where(i => (i.Dt_Validade_Documento - DateTime.Now.Date).TotalDays > 1 && (i.Dt_Validade_Documento - DateTime.Now.Date).TotalDays < 30 && i.Status == EnumStatusDocs.Aprovado).ToList();

                            if (vencimentos.Count > 0)
                            {
                                vencimentos.ForEach(i => { docs += "<li>" + i.Nome + " - validade: " + i.Dt_Validade_Documento.ToShortDateString() + "</li>"; });

                                Usuario user = users.Usuarios.FirstOrDefault(f => f.Cd_Usuario == vencimentos[0].Id_UsuarioCriacao)!;
                                string msg = string.Format(mensagem, user.Nome, docs, item.Nome, item.Empresa!.Nome);

                                Utility.EnviarEmail(msg, user);

                                hasAnexo = true;
                            }
                        }
                    }
                    return new JsonResult(hasAnexo ? AnexoAnswer.DeSucesso("alerta de vencimento enviado") : AnexoAnswer.DeErro("Nenhum documento está proximo do vencimento"));
                }

                return new JsonResult(AnexoAnswer.DeErro("Execução fora da data prevista.")); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("/Operacao/UpdateValidadeResalva")]
        public async Task<AnexoAnswer?> UpdateStatusDocumentoValidadeReslva()
        {
            try
            {
                AnexoAnswer? anexoAnswer = null;
                string motivo = "Rejeitado por falta de ajuste no prazo da resalva";
                AnexoAnswer resposta = servicoAnexo.List(l => l.Status == EnumStatusDocs.Resalva);

                if (resposta.Success)
                {
                    foreach (var item in resposta.Anexos)
                    {
                        DateTime dateValidate = (item.Status == EnumStatusDocs.Resalva ? item.Dt_Criacao.Date.AddDays(1) : item.Dt_Validade_Documento);

                        if ((dateValidate - DateTime.Now.Date).TotalDays <= 0)
                        {
                            anexoAnswer = await Task.Run(() => servicoAnexo.UpdateStatus(item.Cd_Anexo,item.Status == EnumStatusDocs.Aprovado ?  EnumStatusDocs.Expirado : EnumStatusDocs.Rejeitado , "Sistema HDdoc", item.Status == EnumStatusDocs.Resalva ? motivo : null));
                        }
                    }
                }

                return anexoAnswer;

            }
            catch(Exception ex) 
            {
                return  AnexoAnswer.DeErro("Ocorreu um erro: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("/Operacao/GetTreinamentos")]
        public IActionResult GetTreeViewTreinamento()
        {
            try
            {
                TreinamentoAnswer treinamentoAnswer = servicoTreinamento.List(s => s.Status);
                return treinamentoAnswer.Success ? Ok(treinamentoAnswer.Treinamentos) : BadRequest(TreinamentoAnswer.DeErro());
            }
            catch
            {
                return BadRequest(TreinamentoAnswer.DeErro());
            }
        }

        [HttpGet]
        [Route("/Operacao/GetColaborador/{id:int}")]
        public IActionResult GetFuncionario(int id = 0)
        {
            FuncionarioAnswer funcionarioAnswer = servicoFuncionario.Get(p => p.Cd_Funcionario == id && p.Status, new Usuario { Perfil = EnumPerfil.None });

            if (funcionarioAnswer.Success)
            {
                EmpresaAnswer empresaAnswer = servicoEmpresa.Get(g => g.Cd_Empresa == funcionarioAnswer.Funcionario.Id_Empresa, EnumAcao.Nenhum);

                if (empresaAnswer.Success)
                { 
                    Empresa empresa = new Empresa { Cd_Empresa = empresaAnswer.Empresa.Cd_Empresa, Nome = empresaAnswer.Empresa.Nome };
                    funcionarioAnswer.Funcionario.Empresa = empresa;
                }

                if (funcionarioAnswer.Success && empresaAnswer.Success)
                    return Ok(funcionarioAnswer.Funcionario);
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
        public IActionResult DocumentoComplementar(int cd_documento)
        {
           DocumentacaoComplementarAnswer documentacao = servicoDocumentacaoComplementar.Get(s => cd_documento > 0 ? s.Cd_Documentaco_Complementar == cd_documento : s.Cd_Documentaco_Complementar > 0);
           return Ok(documentacao);
        }
    }
}
