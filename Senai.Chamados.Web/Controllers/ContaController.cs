
using Senai.Chamados.Web.ViewModels;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    public class ContaController : Controller
    {
        // GET: Conta
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel Login)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados Inválidos";
                return View();
            }

            return View();
        }

        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel usuario)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados inválidos";
                return View();
            }

            //TODO: Efetuar cadastro banco de dados
            return View();
        }
    }
}