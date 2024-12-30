using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AppAwm.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IChart<ChartAnswer> servico;

        public HomeController(IChart<ChartAnswer> _servico) => servico = _servico;

        public IActionResult Index()
        {
            var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);
            return View(userSession);
        }


        [HttpGet("getChart")]
        [Authorize(Roles = "Funcionario, Terceiro, Administrador")]
        public IActionResult GetChart(int origem)
        {
            // origem 1 :  Colaborador
            // origem 2 :  Empresa

            try
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Start");

                var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                ChartAnswer chartAnswer = servico.Get(s =>
                   (userSession!.Perfil != Models.Enum.EnumPerfil.Administrador ? s.Cd_Empresa_Id == userSession!.Cd_Empresa : s.Cd_Empresa_Id > 0)
                   && (userSession.Perfil != Models.Enum.EnumPerfil.Administrador ? s.Id_UsuarioCriacao == userSession.Cd_Usuario : s.Id_UsuarioCriacao > 0)
                   && (origem == 1 ? s.Cd_Funcionario_Id != null : s.Cd_Funcionario_Id == null)
                   && (s.Status == Models.Enum.EnumStatusDocs.Aprovado || s.Status == Models.Enum.EnumStatusDocs.EmAnalise), userSession!, origem);

                return chartAnswer.Success ? Ok(chartAnswer) : BadRequest(chartAnswer);
            }
            catch (Exception ex)
            {
                return BadRequest(ChartAnswer.DeErro(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet, ActionName("unauthorized")]
        public ActionResult Negado()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
