using Senai.Chamados.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Senai.Chamados.Web.ViewModels.Chamado
{
    public class ListaChamadoViewModel
    {
        public List<ChamadoViewModel> ListaChamados { get; set; }

        public SelectList ListaSetores { get; set; }



        /// <summary>
        /// Carrega a lista de Status a partir de um Enum
        /// </summary>
        /// <returns>Retorna um SelectList com os Status</returns>
        public SelectList CarregaListaSetores()
        {
            var listaSetores = new SelectList(Enum.GetValues(typeof(EnSetor)).Cast<EnSetor>().Select(c =>
                new SelectListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                }).ToList(), "Value", "Text");

            return listaSetores;
        }

        public ListaChamadoViewModel()
        {
            ListaSetores = CarregaListaSetores();
        }
    }
}
