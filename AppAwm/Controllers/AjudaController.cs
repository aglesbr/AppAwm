using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using X.PagedList.Extensions;

namespace AppAwm.Controllers
{
    public class AjudaController(IVideo<VideoAnswer> _servico) : Controller
    {

        private readonly IVideo<VideoAnswer> servico = _servico;

        [HttpGet]
        [Authorize(Roles = "Analista, Terceiro, Administrador")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Terceiro, Administrador")]
        public ActionResult Create(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Start");

            var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

            VideoAnswer resposta = servico.Get(p => p.Cd_Video == id);

            return View(resposta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public IActionResult Create(Video video)
        {
            try
            {
                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);
                if (sessao == null)
                    return RedirectToAction("Index", "Start");

                ModelState.Remove("Cd_Video");

                if (ModelState.IsValid)
                {
                    bool isUri = Regex.IsMatch(video.Url!, @"(http|ftp|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");

                    if (!isUri)
                        return BadRequest(VideoAnswer.DeErro("Informe uma URL válida"));

                    VideoAnswer resposta = servico.Get(s => s.Cd_Video!.Equals(video.Cd_Video));

                    if (resposta.Success)
                    {
                        Video videoAtual = resposta.Video;
                        videoAtual.Titulo = video.Titulo;
                        videoAtual.Descricao = video.Descricao;
                        videoAtual.Url = video.Url;
                        videoAtual.Status = video.Status;

                        resposta = servico.Save(videoAtual, EnumAcao.Editar);
                    }
                    else
                    {
                        resposta = servico.Save(video, EnumAcao.Criar);
                    }

                    return Ok(resposta);
                }

                string[] messages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(VideoAnswer.DeErro(messages));
            }
            catch (Exception ex)
            {
                return BadRequest(VideoAnswer.DeErro(ex.Message));
            }
        }

        [HttpGet]
        [Route("/Ajuda/Search/{skip:int}")]
        [Authorize(Roles = "Terceiro, Administrador")]
        public PartialViewResult Search(string video, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                Video? obj = JsonConvert.DeserializeObject<Video>(video);

                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                VideoAnswer resposta = servico.List(
                     x => (x.Titulo!.ToUpper().Contains(obj.Titulo!.ToUpper())
                     && (sessao.Perfil == EnumPerfil.Administrador ? (obj.StatusFilter.HasValue ? x.Status == obj.StatusFilter > 0 : x.Status == x.Status) : x.Status)));

                var query = resposta.Videos.ToPagedList(skip, 12);
                return PartialView("ListRecord", query);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("/Ajuda/remove/{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult RemoveVideo(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Start");

                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);
                if (sessao != null)
                {
                    var resposta = servico.Get(x => x.Cd_Video == id);

                    if (resposta.Success)
                    {
                        resposta = servico.Remover(resposta.Video);
                    }

                    return resposta.Success ? Ok(resposta) : BadRequest(resposta);
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
