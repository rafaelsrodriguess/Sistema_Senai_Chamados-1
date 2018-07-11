using AutoMapper;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.Util;
using Senai.Chamados.Web.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{

    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult Index()
        {

            //if (!User.IsInRole("Administrador"))
            //{
            //    ViewBag.Erro = "Você não tem permissão para acessar esta tela";
            //    return View();
            //}

            ListaUsuarioViewModel vmListaUsuario = new ListaUsuarioViewModel();

            using(UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                vmListaUsuario.ListaUsuarios = Mapper.Map<List<UsuarioDomain>, List<UsuarioViewModel>>(_repUsuario.Listar());
            }

            return View(vmListaUsuario);
        }

        //TODO: Alterar para string
        [HttpGet]
        public ActionResult Editar(Guid id)
        {
            if(id == Guid.Empty)
            {
                TempData["Erro"] = "Informe o id do usuário";
                return RedirectToAction("Index");
            }

            UsuarioViewModel vmUsuario = new UsuarioViewModel();
 
            using(UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
            {
                vmUsuario = Mapper.Map<UsuarioDomain, UsuarioViewModel>(_repUsuario.BuscarPorId(id));

                if(vmUsuario == null)
                {
                    TempData["Erro"] = "Usuário não encontrado";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(vmUsuario);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(UsuarioViewModel usuario)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados inválidos";
                return View(usuario);
            }

            try
            {
                usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
                usuario.Cep = usuario.Cep.Replace("-","");
                usuario.Senha = Hash.GerarHash(usuario.Senha);

                using(UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
                {
                    _repUsuario.Alterar(Mapper.Map<UsuarioViewModel, UsuarioDomain>(usuario));
                }

                TempData["Erro"] = "Usuário Editado";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View(usuario);
            }
        }

        //TODO: Alterar para string
        [HttpGet]
        public ActionResult Deletar(Guid id)
        {
            if(id == Guid.Empty)
            {
                TempData["Erro"] = "Informe o id do usuário";
                return RedirectToAction("Index");
            }

            using (UsuarioRepositorio _repoUsuario = new UsuarioRepositorio())
            {
                UsuarioViewModel vmUsuario = Mapper.Map<UsuarioDomain, UsuarioViewModel>(_repoUsuario.BuscarPorId(id));

                if(vmUsuario == null)
                {
                    TempData["Erro"] = "Usuário não encontrado";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(vmUsuario);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(UsuarioViewModel usuario)
        {
            if (usuario.Id == Guid.Empty)
            {
                TempData["Erro"] = "Informe o id do usuário";
                return RedirectToAction("Index");
            }

            using (UsuarioRepositorio _repoUsuario = new UsuarioRepositorio())
            {
                UsuarioViewModel vmUsuario = Mapper.Map<UsuarioDomain, UsuarioViewModel>(_repoUsuario.BuscarPorId(usuario.Id));

                if (vmUsuario == null)
                {
                    TempData["Erro"] = "Usuário não encontrado";
                    return RedirectToAction("Index");
                }
                else
                {
                    _repoUsuario.Deletar(Mapper.Map<UsuarioViewModel, UsuarioDomain>(vmUsuario));
                    TempData["Erro"] = "Usuário excluído";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}