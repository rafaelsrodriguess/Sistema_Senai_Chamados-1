using AutoMapper;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Domain.Enum;
using Senai.Chamados.Web.ViewModels.Chamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    //[Authorize]
    public class GraficosController : Controller
    {
        // GET: Graficos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDataStatus()
        {
            try
            {
                ListaChamadoViewModel vmListaChamados = new ListaChamadoViewModel();

                using (ChamadoRepositorio _repoChamado = new ChamadoRepositorio())
                {
                    vmListaChamados.ListaChamados = Mapper.Map<List<ChamadoDomain>, List<ChamadoViewModel>>(_repoChamado.Listar());

                    //if (User.IsInRole("Administrador"))
                    //{
                    //}
                    //else
                    //{
                    //    var claims = User.Identity as ClaimsIdentity;
                    //    var id = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                    //    vmListaChamados.ListaChamados = Mapper.Map<List<ChamadoDomain>, List<ChamadoViewModel>>(_repoChamado.Listar(new Guid(id)));

                    //}
                }

                var grupoStatus = vmListaChamados.ListaChamados
                                    .GroupBy(x => x.Status)
                                    .Select(n => new
                                    {
                                        Status = (EnStatus)n.Key,
                                        Quantidade = n.Count()
                                    }).OrderBy(n => n.Quantidade);

                return Json(new { sucesso = true, resultado = grupoStatus }, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return Json(new { sucesso = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}