using AppAwm.Comando;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using AppAwm.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO.Compression;
using X.PagedList.Extensions;

namespace AppAwm.Controllers
{
    public class AnexoController(IAnexo<AnexoAnswer> _servico, IColaborador<ColaboradorAnswer> _servicoColaborador) : Controller
    {
        private readonly IAnexo<AnexoAnswer> servico = _servico;
        private readonly IColaborador<ColaboradorAnswer> servicoColaborador= _servicoColaborador;

        [HttpPost("/Anexo/AddFiles")]
        [Authorize(Roles = "Colaborador, Terceiro, Administrador")]
        public async Task<IActionResult> AddFile(List<IFormFile> files, string comandoAnexoInformacao)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Start");

                string[] anexoComum = { "anexoComumEmpresa", "anexoComumColaborador" };

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);
                ComandoAnexoInformacao obj = JsonConvert.DeserializeObject<ComandoAnexoInformacao>(comandoAnexoInformacao) ?? new();
                Anexo? anexo = null;

                if (files.Count < 2)
                {
                    var formFile = files[0];

                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);
                        var arq = stream.ToArray();

                        string descricao = string.Empty;

                        if (obj.TipoAnexo > 0)
                            descricao = string.IsNullOrWhiteSpace(obj.Descricao) ? Utility.DocumentacaoComplementares.FirstOrDefault(f => f.Cd_Documentaco_Complementar == obj.TipoAnexo)!.Nome! : obj.Descricao;

                        anexo = new()
                        {
                            Cd_UsuarioCriacao = User.Identity.Name,
                            Dt_Criacao = DateTime.Now,
                            Arquivo = arq,
                            Nome = formFile.FileName,
                            Descricao = obj.TipoAnexo > 0 ? descricao : obj.Descricao,
                            Id_UsuarioCriacao = userSession!.Cd_Usuario,
                        };

                        if (anexoComum.Contains(obj.Scope))
                        {
                            if (obj.Scope == "anexoComumColaborador")
                                anexo.Cd_Funcionario_Id = Convert.ToInt32(obj.CodigoColaborador);
                            else
                                anexo.Cd_Empresa_Id = Convert.ToInt32(obj.CodigoColaborador);

                            anexo.Status = EnumStatusDocs.None;
                        }

                        if (obj.Scope == "empresa")
                        {
                            anexo.Cd_Empresa_Id = Convert.ToInt32(obj.CodigoEmpresa);
                            anexo.TipoAnexo = obj.TipoAnexo;
                            anexo.Status = EnumStatusDocs.Enviado;
                        }

                        if (obj.Scope == "colaborador")
                        {
                            anexo.Cd_Funcionario_Id = Convert.ToInt32(obj.CodigoColaborador);
                            anexo.TipoAnexo = obj.TipoAnexo;
                            anexo.Dt_Validade_Documento = Convert.ToDateTime(obj.DataValidade);
                            anexo.Cd_Empresa_Id = Convert.ToInt32(obj.CodigoEmpresa);
                            anexo.Status = EnumStatusDocs.Enviado;
                        }
                    }

                    AnexoAnswer anexoAnswer = servico.Save(anexo);
                    if (obj.Scope == "colaborador" || obj.Scope == "empresa")
                        servico.Notify();

                    return anexoAnswer.Success ? Ok(anexoAnswer) : BadRequest(AnexoAnswer.DeErro("Nenhum arquivo Selecionado"));
                }

                else
                {
                    using (var archiveStream = new MemoryStream())
                    {
                        using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                        {
                            foreach (var file in files)
                            {
                                var zipArchiveEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                                using var zipStream = zipArchiveEntry.Open();

                                await file.CopyToAsync(archiveStream);

                                zipStream.Write(archiveStream.ToArray(), 0, archiveStream.ToArray().Length);
                            }
                        }

                        byte[] archiveFile = archiveStream.ToArray();

                        anexo = new() { Cd_UsuarioCriacao = User.Identity.Name, Dt_Criacao = DateTime.Now, Arquivo = archiveFile, Nome = $"Arquivos de {obj.Scope} - {obj.Documento}.zip", Descricao = obj.Descricao };

                        if (obj.Scope == "colaborador")
                            anexo.Cd_Funcionario_Id = Convert.ToInt32(obj.CodigoColaborador);

                        if (obj.Scope == "empresa")
                            anexo.Cd_Empresa_Id = Convert.ToInt32(obj.CodigoColaborador);

                        AnexoAnswer anexoAnswer = servico.Save(anexo);

                        return anexoAnswer.Success ? Ok(anexoAnswer) : BadRequest(AnexoAnswer.DeErro("Nenhum arquivo Selecionado"));
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(AnexoAnswer.DeErro(ex.Message));
            }
        }
        
        [HttpGet]
        [Route("/Anexo/Search/{skip:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public PartialViewResult GetAllByEmpresa(string comandoAnexoInformacao, int skip = 1)
        {
            ComandoAnexoInformacao obj = JsonConvert.DeserializeObject<ComandoAnexoInformacao>(comandoAnexoInformacao) ?? new();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                    IEnumerable<Anexo> query = [];
                    AnexoAnswer anexoAnswer;

                    string[] anexoComum = { "anexoComumEmpresa", "anexoComumColaborador" };

                    if (anexoComum.Contains(obj.Scope))
                    {
                        anexoAnswer = servico.List(x => (obj.Scope.Equals("anexoComumColaborador")
                            ? x.Cd_Funcionario_Id == Convert.ToInt32(obj.CodigoColaborador)
                            : x.Cd_Funcionario_Id == null && x.Cd_Empresa_Id == Convert.ToInt32(obj.CodigoColaborador))
                            && (userSession!.Perfil == EnumPerfil.Administrador ? x.Id_UsuarioCriacao > 0 : x.Id_UsuarioCriacao == userSession.Cd_Usuario)
                            && x.Status == EnumStatusDocs.None);
                    }
                    else
                    {
                        anexoAnswer = servico.List(x => 
                        (obj.Scope == "empresa" ? x.Cd_Empresa_Id == Convert.ToInt32(obj.CodigoEmpresa) : x.Cd_Funcionario_Id == Convert.ToInt32(obj.CodigoColaborador))
                        && (obj.Scope == "empresa" ? x.TipoAnexo >= 28 : x.TipoAnexo < 28)
                        && (userSession!.Perfil == EnumPerfil.Administrador ? x.Id_UsuarioCriacao > 0 : x.Id_UsuarioCriacao == userSession.Cd_Usuario)
                        && x.Status != EnumStatusDocs.None);
                    }

                    var itemDocumento = string.Join(',', [.. anexoAnswer.Anexos.Select(s => s.TipoAnexo.ToString())]);

                    query = anexoAnswer.Anexos.Select(s => new Anexo
                    {
                        Cd_Anexo = s.Cd_Anexo,
                        Nome = s.Nome,
                        Descricao = s.Descricao,
                        Cd_Funcionario_Id = s.Cd_Funcionario_Id,
                        Cd_UsuarioAnalista = s.Cd_UsuarioAnalista,
                        Cd_Empresa_Id = s.Cd_Empresa_Id,
                        Status = s.Status,
                        MotivoRejeicao = s.MotivoRejeicao,
                        MotivoResalva = s.MotivoResalva,
                        TipoAnexo = s.TipoAnexo,
                        Dt_Validade_Documento = s.Dt_Validade_Documento,
                        Dt_Criacao = s.Dt_Criacao,
                        CodigosDocumentos = itemDocumento,
                        TemHistorico = itemDocumento.Split(',').Count(c => c == s.TipoAnexo.ToString()) > 1
                    }).ToList();



                    var queryGroup = (obj.Scope == "colaborador" || obj.Scope == "empresa")
                        ? query.OrderByDescending(ob => ob.Dt_Criacao).GroupBy(gb => gb.TipoAnexo).Select(ss =>  ss.FirstOrDefault()).ToPagedList(skip, 10)
                        : query.ToPagedList(skip, 10);

                    if (obj.Scope == "colaborador")
                        return PartialView("ListAnexoColaboradorRecord", queryGroup);

                    return PartialView("ListAnexoRecord", queryGroup);
                }

                return PartialView(obj.Scope != "colaborador" ? "ListAnexoRecord" : "ListAnexoColaboradorRecord", BadRequest(AnexoAnswer.DeErro("Usuario não esta autenticado")));
            }
            catch (Exception ex)
            {
                return PartialView(obj.Scope != "colaborador" ? "ListAnexoRecord" : "ListAnexoColaboradorRecord", BadRequest(AnexoAnswer.DeErro(ex.Message)));
            }
        }

        [HttpGet]
        [Route("/Anexo/historico/")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public PartialViewResult GetHistoricoAnexo(string comandoAnexoInformacao)
        {
            ComandoAnexoInformacao obj = JsonConvert.DeserializeObject<ComandoAnexoInformacao>(comandoAnexoInformacao) ?? new();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                    List<EnumStatusDocs> statusDocs = [EnumStatusDocs.Expirado, EnumStatusDocs.Rejeitado];

                    AnexoAnswer anexoAnswer = servico.List(x =>
                        (obj.Scope == "empresa" ? x.Cd_Empresa_Id : x.Cd_Funcionario_Id) == Convert.ToInt32(obj.CodigoColaborador)
                        // && (obj.Scope == "colaborador" ? statusDocs.Contains((EnumStatusDocs)x.Status!) : x.Status == null)
                        && statusDocs.Contains((EnumStatusDocs)x.Status!)
                        && x.TipoAnexo == obj.TipoAnexo);

                    var query = anexoAnswer.Anexos.OrderBy(ob => ob.Cd_Anexo).Select(s => new Anexo
                    {
                        Cd_Anexo = s.Cd_Anexo,
                        Nome = s.Nome,
                        Descricao = s.Descricao,
                        Status = s.Status,
                        MotivoRejeicao = s.MotivoRejeicao,
                        Cd_UsuarioAnalista = s.Cd_UsuarioAnalista,
                        Dt_Criacao = s.Dt_Criacao
                    }).ToList();

                    query.RemoveAll(r => r.Cd_Anexo == obj.CodigoAnexo);

                    return PartialView("ListAnexoHistoricoRecord", query);
                }
                return PartialView("ListAnexoHistoricoRecord", BadRequest(AnexoAnswer.DeErro("Usuario não esta autenticado")));
            }
            catch (Exception ex)
            {
                return PartialView("ListAnexoHistoricoRecord", BadRequest(AnexoAnswer.DeErro(ex.Message)));
            }

        }

        [HttpDelete("/Anexo/remove/{id:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public IActionResult RemoveFile(int id)
        {
            try
            {
                AnexoAnswer anexoAnswer = servico.List(s => s.Cd_Anexo == id);

                if (anexoAnswer.Success)
                {
                    if (anexoAnswer.Anexos[0].Status is null)
                    {
                        anexoAnswer = servico.Remove(anexoAnswer.Anexos[0]);
                        return anexoAnswer.Success ? Ok(anexoAnswer) : BadRequest(anexoAnswer);
                    }
                    else
                    {
                        if ((EnumStatusDocs.Enviado | EnumStatusDocs.Rejeitado).HasFlag(anexoAnswer.Anexos[0].Status!))
                        {
                            anexoAnswer = servico.Remove(anexoAnswer.Anexos[0]);

                            return anexoAnswer.Success ? Ok(anexoAnswer) : BadRequest(anexoAnswer);
                        }
                        else
                        {
                            return BadRequest(AnexoAnswer.DeErro($"Esse documento já está sendo validado"));
                        }
                    }
                }

                return BadRequest(AnexoAnswer.DeErro("Não foi possivel remover o anexo"));
            }
            catch (Exception ex)
            {
                return BadRequest(AnexoAnswer.DeErro($"Ocorreu um ERRO {ex.Message} "));
            }
        }

        [HttpGet]
        [Route("/Anexo/downloadFile/{id:int}")]
        [Authorize(Roles = "Analista, Terceiro, Administrador")]
        public IActionResult? DownloadFile(int id, bool isApp = false)
        {
            try
            {
                //TipoAnexo == 100 (esse anexo é do software do analista) NUNCA. JAMAIS EXLUIR ESSE REGISTRO
                AnexoAnswer anexoAnswer = servico.List(s => isApp ? s.Status == 0 && s.TipoAnexo == 100 : s.Cd_Anexo == id, true);

                if (anexoAnswer.Success)
                {
                    return File(anexoAnswer.Anexos[0].Arquivo!, System.Net.Mime.MediaTypeNames.Application.Octet, anexoAnswer.Anexos[0].Nome);
                }

                return File([], "application/pdf");
            }
            catch
            {
                return null;
            }
        }


        [HttpGet]
        [Route("/Anexo/loadFile")]
        [Authorize(Roles = "Administrador")]
        public IActionResult? OpenDocument(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest("Usuario não autenticado");

            var file = servico.List(s => s.Cd_Anexo == id, true);

            if (file.Success)
            {
                var anexo = file.Anexos[0];
                if (anexo.Status == EnumStatusDocs.Enviado)
                    servico.UpdateStatus(id, EnumStatusDocs.EmAnalise, User.Identity.Name!);

                if (file.Success)
                    return Ok(file.Anexos[0].Arquivo);
            }

            return BadRequest(file);
        }

        [HttpPut]
        [Route("/Anexo/updateStatus")]
        [Authorize(Roles = "Administrador")]
        public IActionResult ChangeStatusDocument(string obj)
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest("Usuario não está autenticadp");

            var newStatus = JsonConvert.DeserializeAnonymousType(obj, new { id = 0, status = 0, message = string.Empty, isRevoga = false });


            var file = servico.List(s => s.Cd_Anexo == newStatus!.id);

            if (file.Success)
            {

                file = servico.UpdateStatus(newStatus!.id, (EnumStatusDocs)newStatus.status, User.Identity.Name!, newStatus.message, newStatus.isRevoga);

                if (file.Success)
                    return Ok(file);
            }

            return BadRequest(file);
        }

        [HttpPut]
        [Route("/Anexo/updateFoto")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public IActionResult ChangeFotoAsync(IFormFile file, int cd_funcionario)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return BadRequest("Usuario não autenticado");

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                Colaborador? obj = servicoColaborador.List(g => g.Cd_Funcionario == cd_funcionario).Colaboradore.FirstOrDefault();

                if (obj != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        obj.Foto = stream.ToArray();
                        var retorno = servicoColaborador.UpdateFoto(obj);

                        return Ok(retorno > 0 ? ColaboradorAnswer.DeSucesso(EnumAcao.Editar) : ColaboradorAnswer.DeFalha());
                    }
                }
                return BadRequest(ColaboradorAnswer.DeErro("Não exite um registro para atualizar a foto"));
            }
            catch (Exception ex)
            {
                return BadRequest(ColaboradorAnswer.DeErro("Ocorreu um erro ao tentar atualizar a foto - Erro " + ex.Message));
            }

        }

        [HttpGet]
        [Route("/Anexo/listDocuments/{Id:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public JsonResult GetDocumentoComplementar(int id, bool tipoAnexoEmpresa = false, int? cd_empresa =  null)
        {
            try
            {
                var documentos = !tipoAnexoEmpresa ? servico.DocumentacaoComplementar(id, cd_empresa) : servico.DocumentacaoComplementar(dc => dc.Cd_DocumentoComplementar_Id == id.ToString());

                return documentos.Count > 0 ? Json(new { documentos, success = true }) : Json(new { erro = "nenhum item encontrado", success = false });
            }
            catch (Exception ex)
            {

                return Json(new { erro = ex.Message, success = false });
            }
        }
    }
}
