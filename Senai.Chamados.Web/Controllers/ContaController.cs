
using AutoMapper;
using Senai.Chamados.Data.Contexto;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Domain.Enum;
using Senai.Chamados.Web.Util;
using Senai.Chamados.Web.ViewModels;
using Senai.Chamados.Web.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
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
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel Login)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados Inválidos";
                return View();
            }

            using(UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                UsuarioDomain objUsuario = _repUsuario.Login(Login.Email, Hash.GerarHash(Login.Senha));

                if(objUsuario != null)
                {
                    var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, objUsuario.Nome),
                        new Claim(ClaimTypes.Email, objUsuario.Email),
                        new Claim(ClaimTypes.NameIdentifier,objUsuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, objUsuario.TipoUsuario.ToString()),
                        //Defini uma Claim com um novo tipo
                        new Claim("Telefone", objUsuario.Telefone.ToString())
                    }, "ApplicationCookie");

                    Request.GetOwinContext().Authentication.SignIn(identities: identity);

                    return RedirectToAction("Index", "Usuario");
                }
                else
                {
                    ViewBag.Erro = "Usuário ou senha inválidos. Tente novamente";
                    return View(Login);
                }
            }

        }

        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
            UsuarioViewModel objCadastrarUsuario = new UsuarioViewModel();

            objCadastrarUsuario.ListaSexo =  ListaSexo();
            
            return View(objCadastrarUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarUsuario(UsuarioViewModel usuario)
        {
            usuario.ListaSexo = ListaSexo();

            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados inválidos";
                return View(usuario);
            }

            try
            {
                usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-","");
                usuario.Cep = usuario.Cep.Replace("-", "");
                usuario.TipoUsuario = EnTipoUsuario.Padrao;
                usuario.Senha = Hash.GerarHash(usuario.Senha);

                using(UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
                {
                    _repUsuario.Inserir(Mapper.Map<UsuarioViewModel, UsuarioDomain>(usuario));
                }

                TempData["Mensagem"] = "Usuário cadastrado";
                return RedirectToAction("Login");
            }
            catch (System.Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View(usuario);
            }
        }

        private SelectList ListaSexo()
        {
            return new SelectList(
             new List<SelectListItem>
             {
              new SelectListItem { Text = "Masculino", Value = "1"},
              new SelectListItem { Text= "Feminino", Value = "2"},
             }, "Value", "Text");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");

            return RedirectToAction("Login");
        }
    }
}