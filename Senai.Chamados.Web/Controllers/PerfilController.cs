using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.ViewModels.Conta;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AlterarSenha()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel senha)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Erro = "Dados inválidos. Verifique!";
                    return View();
                }

                //Obtêm as Claims do usuário logado
                var identity = User.Identity as ClaimsIdentity;
                //Pega o valor do Id do usuário
                var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                //Obtêm o valor de uma Claim nâo definida
                var telefone = identity.Claims.FirstOrDefault(x => x.Type == "Telefone").Value;

                //Cria uma instancia de UsuarioRepositorio
                using(UsuarioRepositorio  objRepoUsuario = new UsuarioRepositorio())
                {
                    //Busca o usuário pelo seu Id
                    UsuarioDomain objUsuario = objRepoUsuario.BuscarPorId(new Guid(id));

                    //Verifico se a senha informada é igual a Atual
                    if(senha.SenhaAtual != objUsuario.Senha)
                    {
                        //Senha inválida eu informo ao usuário
                        ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                        return View();
                    }
                    //Atribuimos o valor da nova senha ao objeto usuário
                    objUsuario.Senha = senha.NovaSenha;
                    //Alteramos o usuário no banco
                    objRepoUsuario.Alterar(objUsuario);
                    //Definimos a mensagem que irá aparecer na tela
                    TempData["Sucesso"] = "Senha Alterada";
                    //Retornamos ao Controller Usuário, Index
                    return RedirectToAction("Index", "Usuario");
                }               
            }
            catch (System.Exception ex)
            {
                ViewBag.Erro = "Ocorreu um erro " + ex.Message;
                return View();
            }
        }
    }
}