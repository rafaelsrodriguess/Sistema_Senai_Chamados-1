using AutoMapper;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.ViewModels.Chamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{

    [Authorize]
    public class ChamadoController : Controller
    {
        // GET: Chamado
        [HttpGet]
        public ActionResult Index()
        {
            ListaChamadoViewModel vmListaChamados = new ListaChamadoViewModel();

            using (ChamadoRepositorio _repoChamado = new ChamadoRepositorio())
            {
                if (User.IsInRole("Administrador"))
                {
                    vmListaChamados.ListaChamados = Mapper.Map<List<ChamadoDomain>, List<ChamadoViewModel>>(_repoChamado.Listar());
                }
                else
                {
                    var claims = User.Identity as ClaimsIdentity;
                    var id = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                    vmListaChamados.ListaChamados = Mapper.Map<List<ChamadoDomain>, List<ChamadoViewModel>>(_repoChamado.Listar(new Guid(id)));

                }

            }

            return View(vmListaChamados);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            ChamadoViewModel vmChamado = new ChamadoViewModel();

            return View(vmChamado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(ChamadoViewModel chamado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Erro = "Dados inválidos";
                    return View(chamado);
                }

                using (ChamadoRepositorio objRepoChamado = new ChamadoRepositorio())
                {
                    var identity = User.Identity as ClaimsIdentity;
                    var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    chamado.IdUsuario = new Guid(id);

                    objRepoChamado.Inserir(Mapper.Map<ChamadoViewModel, ChamadoDomain>(chamado));
                }

                TempData["Sucesso"] = "Chamado Cadastrado. Aguarde!!!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View(chamado);
            }
        }

        [HttpGet]
        public ActionResult Editar(string id)
        {
            //Instancia a ViewModel chamado
            ChamadoViewModel objChamado = new ChamadoViewModel();

            try
            {
                if(id == null)
                {
                    TempData["Erro"] = "Id não identificado";
                    return RedirectToAction("Index");
                }

                using(ChamadoRepositorio objRepoChamado = new ChamadoRepositorio())
                {
                    //Busca o chamado pelo Id
                    objChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(objRepoChamado.BuscarPorId(new Guid(id)));

                    if(objChamado == null)
                    {
                        TempData["Erro"] = "Chamado não encontrado";
                        return RedirectToAction("Index");
                    }

                    #region Buscar Id Usuario
                    var identity = User.Identity as ClaimsIdentity;
                    var idUsuario = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    #endregion

                    if(User.IsInRole("Administrador") || idUsuario == objChamado.IdUsuario.ToString())
                        return View(objChamado);
                    else
                    {
                        TempData["Erro"] = "Este chamado pertence a outro usuário.";
                        return RedirectToAction("Index");
                    }
                }
            }

            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View(objChamado);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ChamadoViewModel chamado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Erro = "Dados inválidos";
                    return View(chamado);
                }

                using (ChamadoRepositorio objRepoChamado = new ChamadoRepositorio())
                {
                    objRepoChamado.Alterar(Mapper.Map<ChamadoViewModel, ChamadoDomain>(chamado));
                    TempData["Sucesso"] = "Chamado alterado";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Excluir(string id)
        {
            try
            {
                if(id == null)
                {
                    TempData["Erro"] = "Informe o id do chamado";
                    return RedirectToAction("Index");
                }

                ChamadoViewModel objChamado = new ChamadoViewModel();

                using (ChamadoRepositorio objRepoChamado = new ChamadoRepositorio())
                {
                    objChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(objRepoChamado.BuscarPorId(new Guid(id)));

                    if(objChamado == null)
                    {
                        TempData["Erro"] = "Chamado não encontrado";
                        return RedirectToAction("Index");
                    }

                    #region Buscar Id Usuario
                    var identity = User.Identity as ClaimsIdentity;
                    var idUsuario = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    #endregion

                    if (User.IsInRole("Administrador") || idUsuario == objChamado.IdUsuario.ToString())
                    {
                        return View(objChamado);
                    }

                    TempData["Erro"] = "Você não possui permissão para excluir este chamado";
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View();
            }
        }
    }
}