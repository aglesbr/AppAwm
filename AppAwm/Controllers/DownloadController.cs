using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList.Extensions;

namespace AppAwm.Controllers
{
    public class DownloadController(IDownload<DownloadAnswer> _servico) : Controller
    {
        private readonly IDownload<DownloadAnswer> servico = _servico;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/download/Search/{skip:int}")]
        public PartialViewResult Search(string download, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                Download? obj = JsonConvert.DeserializeObject<Download>(download);

                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                DownloadAnswer resposta = servico.GetAll(
                    x => (x.Nome!.ToUpper().StartsWith(obj.Nome!.ToUpper()))
                && (x.Descricao.ToUpper().StartsWith(obj.Descricao.ToUpper())));

                var query = resposta.Downloads.ToPagedList(skip, 12);
                return PartialView("ListRecord", query);
            }
            catch (Exception ex)
            {
                return PartialView("ListRecord", BadRequest("Ocorreu um erro na consulta: " + ex.Message));
            }
        }

        [HttpPost]
        [Route("/download/upload")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UploadArquivosDeSistema(List<IFormFile> files, string descricao)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Start");

                Download? download = null;
                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                var formFile = files[0];

                using var stream = new MemoryStream();
                await formFile.CopyToAsync(stream);

                download = new Download() { Cd_UsuarioCriacao = userSession!.Nome, Descricao = descricao,  Anexo = stream.ToArray(), Nome = formFile.FileName };

                DownloadAnswer resposta = servico.Save(download);


                return Json(resposta.Success ? resposta : DownloadAnswer.DeErroOuVazio(resposta.Message));
            }
            catch (Exception ex)
            {
                return Json(DownloadAnswer.DeErroOuVazio($"Ocorreu um erro na importação de colaboradores {ex.Message}"));
            }
        }


        [HttpDelete("/download/remove/{id:int}")]
        [Authorize(Roles = "Administrador")]
        public JsonResult RemoveFile(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    Json(DownloadAnswer.DeErroOuVazio("Usuario não está autenticado para exxa operação"));

                DownloadAnswer resposta = servico.Get(id);

                return resposta.Success ? Json(servico.Remover(resposta.Download)) : Json(DownloadAnswer.DeErroOuVazio("Não foi possivel localizar o arquivo para exlusão."));
                
            }
            catch (Exception ex)
            {
                return Json(AnexoAnswer.DeErro($"Ocorreu um ERRO {ex.Message}"));
            }
        }


        [HttpGet]
        [Route("/download/downloadFile/{id:int}")]
        [Authorize(Roles = "Administrador, Terceiro, Analista")]
        public IActionResult? DownloadFile(int id)
        {
            try
            {
                DownloadAnswer resposta = servico.Get(id);

                if (resposta.Success)
                {
                    return File(resposta.Download!.Anexo!, System.Net.Mime.MediaTypeNames.Application.Octet, resposta.Download.Nome);
                }

                ViewBag.FileNotFound = "O sistema não conseguiu localizar o arquivo";

                return View("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}
