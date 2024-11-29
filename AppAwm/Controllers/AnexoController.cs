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
    public class AnexoController(IAnexo<AnexoAnswer> _servico, IFuncionario<FuncionarioAnswer> _servicoFuncionario) : Controller
    {
        private readonly IAnexo<AnexoAnswer> servico = _servico;
        private readonly IFuncionario<FuncionarioAnswer> servicoFuncionario = _servicoFuncionario;

        [HttpPost("/Anexo/AddFiles")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public async Task<IActionResult> AddFile(List<IFormFile> files, string comandoAnexoInformacao)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Start");

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
                        
                        anexo = new() 
                        { 
                            Cd_UsuarioCriacao = User.Identity.Name, 
                            Dt_Criacao = DateTime.Now, 
                            Arquivo = arq, 
                            Nome = formFile.FileName, 
                            Descricao = obj.Descricao,
                            Id_UsuarioCriacao = userSession!.Cd_Usuario,
                            Cd_Cliente = userSession.Cd_Cliente_Id
                        };

                        if (obj.Scope == "funcionario")
                            anexo.Cd_Funcionario_Id = Convert.ToInt32(obj.Codigo);

                        if (obj.Scope == "empresa")
                            anexo.Cd_Empresa_Id = Convert.ToInt32(obj.Codigo);

                        if (obj.Scope == "colaborador")
                        {
                            anexo.Cd_Funcionario_Id = Convert.ToInt32(obj.Codigo);
                            anexo.TipoAnexo = obj.TipoAnexo;
                            anexo.Dt_Validade_Documento = Convert.ToDateTime(obj.DataValidade);
                            anexo.Cd_Empresa_Id = Convert.ToInt32(obj.CodigoEmpresa);
                            anexo.Status = EnumStatusDocs.Enviado;
                        }
                    }

                    AnexoAnswer anexoAnswer = servico.Save(anexo);
                    if (!string.IsNullOrWhiteSpace(anexo.TipoAnexo))
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

                        if (obj.Scope == "funcionario")
                            anexo.Cd_Funcionario_Id = Convert.ToInt32(obj.Codigo);

                        if (obj.Scope == "empresa")
                            anexo.Cd_Empresa_Id = Convert.ToInt32(obj.Codigo);

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

                    AnexoAnswer anexoAnswer = servico.List(x => x.Cd_Cliente == userSession!.Cd_Cliente_Id
                        && (obj.Scope == "empresa" ? x.Cd_Empresa_Id : x.Cd_Funcionario_Id) == Convert.ToInt32(obj.Codigo)
                        && (obj.Scope == "colaborador" ? x.Status != null : x.Status == null)
                        && x.TipoAnexo == (string.IsNullOrWhiteSpace(obj.TipoAnexo) ? x.TipoAnexo : obj.TipoAnexo)
                        && (string.IsNullOrWhiteSpace(obj.Descricao) ? x.Descricao == x.Descricao : x.Descricao!.StartsWith(obj.Descricao)));
                    
                    var itemDocumento = string.Join(',',[.. anexoAnswer.Anexos.Select(s => s.TipoAnexo)]);
                    
                        var query = obj.Scope == "empresa"
                        ? anexoAnswer.Anexos.Select(s => new Anexo { Cd_Anexo = s.Cd_Anexo, Nome = s.Nome, Descricao = s.Descricao, Cd_Empresa_Id = s.Cd_Empresa_Id }).ToList().ToPagedList(skip, 10)
                        : obj.Scope == "funcionario"
                        ? anexoAnswer.Anexos.Select(s => new Anexo { Cd_Anexo = s.Cd_Anexo, Nome = s.Nome, Descricao = s.Descricao, Cd_Funcionario_Id = s.Cd_Funcionario_Id }).ToList().ToPagedList(skip, 10)
                        : anexoAnswer.Anexos.Select(s => new Anexo { Cd_Anexo = s.Cd_Anexo, Nome = s.Nome, Descricao = s.Descricao, Cd_Funcionario_Id = s.Cd_Funcionario_Id, Status = s.Status, 
                            TipoAnexo = s.TipoAnexo, Dt_Validade_Documento = s.Dt_Validade_Documento, CodigosDocumentos = itemDocumento}).ToList().ToPagedList(skip, 10);

                    if (obj.Scope == "colaborador")
                        return PartialView("ListAnexoColaboradorRecord", query);

                    return PartialView("ListAnexoRecord", query);
                }
                return PartialView(obj.Scope != "colaborador" ? "ListAnexoRecord" : "ListAnexoColaboradorRecord", BadRequest(AnexoAnswer.DeErro("Usuario não esta autenticado")));
            }
            catch (Exception ex)
            {
                return PartialView(obj.Scope != "colaborador" ? "ListAnexoRecord" : "ListAnexoColaboradorRecord", BadRequest(AnexoAnswer.DeErro(ex.Message)));
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
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public IActionResult? DownloadFile(int id)
        {
            try
            {
                AnexoAnswer anexoAnswer = servico.List(s => s.Cd_Anexo == id);

                if (anexoAnswer.Success)
                {
                    return File(anexoAnswer.Anexos[0].Arquivo!, System.Net.Mime.MediaTypeNames.Application.Octet, anexoAnswer.Anexos[0].Nome);
                }

                return null;
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

            var file = servico.List(s => s.Cd_Anexo == id);

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
            if (User.Identity.IsAuthenticated)
                return BadRequest("Usuario não está autenticadp");

            var newStatus = JsonConvert.DeserializeAnonymousType(obj, new { id = 0, status = 0, message = string.Empty });

            if (!User.Identity.IsAuthenticated)
                return BadRequest("Usuario não autenticado");

            var file = servico.List(s => s.Cd_Anexo == newStatus!.id);

            if (file.Success)
            {

                file = servico.UpdateStatus(newStatus!.id, (EnumStatusDocs)newStatus.status, User.Identity.Name!, newStatus.message);

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

                Funcionario? obj = servicoFuncionario.List(g => g.Cd_Funcionario == cd_funcionario && g.Cd_Cliente == userSession!.Cd_Cliente_Id).Funcionarios.FirstOrDefault();

                if (obj != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        obj.Foto = stream.ToArray();
                        var retorno = servicoFuncionario.UpdateFoto(obj);

                        return Ok(retorno > 0 ? FuncionarioAnswer.DeSucesso(EnumAcao.Editar) : FuncionarioAnswer.DeFalha());
                    }
                }
                        return BadRequest(FuncionarioAnswer.DeErro("Não exite um registro para atualizar a foto"));
            }
            catch (Exception ex)
            {
                return BadRequest(FuncionarioAnswer.DeErro("Ocorreu um erro ao tentar atualizar a foto - Erro " + ex.Message));
            }

        }

        [HttpGet]
        [Route("/Anexo/listDocuments/{Id:int}")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public JsonResult GetDocumentoComplementar(int id)
        {
            try
            {
                var documentos = servico.DocumentacaoComplementar(id);

                return documentos.Count > 0 ?  Json(new { documentos, success = true }) : Json(new { erro = "nenhum item encontrado", success = false });
            }
            catch (Exception ex)
            {

                return Json(new {erro = ex.Message, success = false });
            }
        }
    }
}
