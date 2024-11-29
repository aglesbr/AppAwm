using AppAwm.Models;
using AppAwm.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AppAwm.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var jSonUsuario = HttpContext.Session.GetString("UserAuth");
            var usuario = JsonSerializer.Deserialize<Usuario>(jSonUsuario!);

            return View(usuario);
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
