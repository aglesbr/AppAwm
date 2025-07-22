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
    [Authorize]
    public class ClienteController(ICliente<ClienteAnswer> _servico) : Controller
    {

        private readonly ICliente<ClienteAnswer> servico = _servico;

        // GET: ClienteController
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("/cliente/Search/{skip:int}")]
        [Authorize(Roles = "Master, Terceiro, Administrador")]
        public PartialViewResult Search(string cliente, int skip = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return PartialView("ListRecord", BadRequest("Usuario não autenticado"));

                Cliente? obj = JsonConvert.DeserializeObject<Cliente>(cliente);

                if (!string.IsNullOrWhiteSpace(obj.Cnpj))
                    obj.Cnpj = Regex.Replace(obj.Cnpj, @"[^0-9$]", string.Empty);


                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

                ClienteAnswer resposta = servico.List(
                     x => (x.Nome!.ToUpper().StartsWith(obj.Nome!.ToUpper())
                     && x.Cd_UsuarioCriacao == (sessao.Perfil == EnumPerfil.Administrador ? x.Cd_UsuarioCriacao : sessao.Nome)
                     && x.Cnpj!.StartsWith(obj.Cnpj ?? x.Cnpj)
                     && (obj.StatusFilter.HasValue ? x.Status == obj.StatusFilter > 0 : x.Status == x.Status)));

                var query = resposta.Clientes.ToPagedList(skip, 12);
                return PartialView("ListRecord", query);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        [Authorize(Roles = "Master, Terceiro, Administrador")]
        public ActionResult Create(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Start");

            var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);

            ClienteAnswer clienteAnswer = servico.Get(p => p.Cd_Cliente == id && p.Cd_UsuarioCriacao == (userSession!.Perfil == EnumPerfil.Administrador ? p.Cd_UsuarioCriacao : userSession.Nome!));
            clienteAnswer.Cliente.PlanoPacoteVidas = clienteAnswer.Cliente.PlanoPacoteVidas != null ? clienteAnswer.Cliente.PlanoPacoteVidas!.Replace("PV-", "") : clienteAnswer.Cliente.PlanoPacoteVidas;

            return View(clienteAnswer);
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Master, Terceiro, Administrador")]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                var sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserAuth")!);
                if (sessao == null)
                    return RedirectToAction("Index", "Start");

                ModelState.Remove("Cd_Cliente");

                if (ModelState.IsValid)
                {
                    cliente.Cnpj = Regex.Replace(cliente.Cnpj!, @"[^\d]", string.Empty);
                    var emp = servico.Get(s => s.Cnpj!.Equals(cliente.Cnpj));

                    if (cliente.Cd_Cliente > 0)
                    {
                        cliente.Cd_UsuarioAtualizacao = User.Identity?.Name ?? "ANONYMOUS";
                        cliente.Dt_Atualizacao = DateTime.Now;
                    }
                    else
                    {
                        cliente.Cnpj = Regex.Replace(cliente.Cnpj!, @"[^\d]", string.Empty);
                        cliente.Cd_UsuarioCriacao = User.Identity?.Name ?? "ANONYMOUS";

                        if (emp.Success)
                            return BadRequest(ClienteAnswer.DeFalha("CNPJ já cadastrado"));
                    }

                    cliente.PlanoPacoteVidas = "PV-" + cliente.PlanoPacoteVidas;
                    cliente.Cd_UsuarioCriacao = emp.Success ? emp.Cliente.Cd_UsuarioCriacao : cliente.Cd_UsuarioCriacao;

                    ClienteAnswer clienteAnswer = servico.Save(cliente, (cliente.Cd_Cliente == 0 ? EnumAcao.Criar : EnumAcao.Editar));

                    return clienteAnswer.Success ? Ok(clienteAnswer) : BadRequest(clienteAnswer);

                }
                string[] messages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(ClienteAnswer.DeErro(messages));
            }
            catch (Exception ex)
            {
                return BadRequest(ClienteAnswer.DeErro(ex.Message));
            }
        }
    }
}
