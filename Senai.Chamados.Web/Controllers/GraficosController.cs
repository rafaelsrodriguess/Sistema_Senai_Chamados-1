using AutoMapper;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Domain.Enum;
using Senai.Chamados.Web.ViewModels.Chamado;
using Senai.Chamados.Web.ViewModels.Grafico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    [Authorize]
    public class GraficosController : Controller
    {
        // GET: Graficos
        public ActionResult Index()
        {
            try
            {

                ListaGraficoViewModel vmGrafico = new ListaGraficoViewModel();
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

                #region Grafico Status
                //Faz o Agrupamento dos dados por Status
                var grupoStatus = vmListaChamados.ListaChamados
                               .GroupBy(x => x.Status)
                               .Select(n => new
                               {
                                   Status = RetornaStatus(n.Key),
                                   Quantidade = Convert.ToDouble (n.Count())
                               }).OrderBy(n => n.Quantidade);
                //Atribui as labels que serão mostradas no grafico
                vmGrafico.GraficoStatus.Labels = grupoStatus.Select(x => x.Status).ToArray();
                //Atribuir os dasdos que serão apresentados no grafico
                vmGrafico.GraficoStatus.Data = grupoStatus.Select(x => x.Quantidade).ToArray();
                #endregion


                #region Grafico Setor
                //Faz o Agrupamento dos dados por Setor
                var grupoSetor = vmListaChamados.ListaChamados
                               .GroupBy(x => x.Setor)
                               .Select(n => new
                               {
                                   Setor = RetornaSetor(n.Key),
                                   Quantidade = Convert.ToDouble(n.Count())
                               }).OrderBy(n => n.Quantidade);
                //Atribui as labels que serão mostradas no grafico
                vmGrafico.GraficoSetor.Labels = grupoSetor.Select(x => x.Setor).ToArray();
                //Atribuir os dasdos que serão apresentados no grafico
                vmGrafico.GraficoSetor.Data = grupoSetor.Select(x => x.Quantidade).ToArray();
                #endregion
                return View(vmGrafico);
            }


            catch (System.Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View();
            }
        }

                    private string RetornaSetor(EnSetor setor)
        {
            return setor.ToString();
        }
                   private string RetornaStatus(EnStatus status)
            {
            switch (status)
            {
                case EnStatus.Aguardando:
                    return "Aguardando";
                case EnStatus.Iniciado:
                    return "Iniciando";
                case EnStatus.Finalizado:
                    return "Finalizando";
                default:
                    break;
            }

            return null;
            }



    }

   
    
}