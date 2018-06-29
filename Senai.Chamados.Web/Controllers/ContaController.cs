
using AutoMapper;
using Senai.Chamados.Data.Contexto;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.ViewModels;
using System;
using System.Collections.Generic;
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

            using(UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                UsuarioDomain objUsuario = _repUsuario.Login(Login.Email, Login.Senha);

                if(objUsuario != null)
                {
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
            CadastrarUsuarioViewModel objCadastrarUsuario = new CadastrarUsuarioViewModel();
            //objCadastrarUsuario.Nome = "Fernando Henrique";
            //objCadastrarUsuario.Email = "fernando.guerra@corujasdev.com.br";

            objCadastrarUsuario.Sexo =  ListaSexo();


            return View(objCadastrarUsuario);
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel usuario)
        {
            usuario.Sexo = ListaSexo();

            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados inválidos";
                return View(usuario);
            }

            try
            {
                usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-","");
                usuario.Cep = usuario.Cep.Replace("-", "");
                
                using(UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
                {
                    _repUsuario.Inserir(Mapper.Map<CadastrarUsuarioViewModel, UsuarioDomain>(usuario));
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
    }
}