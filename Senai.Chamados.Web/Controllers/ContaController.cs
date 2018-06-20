using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    public class ContaController : Controller
    {
        // GET: Conta
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CadastrarUsuario()
        {
            return View();
        }
    }
}