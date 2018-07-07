using Senai.Chamados.Domain.Enum;
using Senai.Chamados.Web.ViewModels.Usuario;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Senai.Chamados.Web.ViewModels.Chamado
{
    public class ChamadoViewModel : BaseViewModel
    {

        public ChamadoViewModel()
        {
            ListaSetores = CarregaListaSetores();
            ListaStatus = CarregaListaStatus();
        }

        [Required(ErrorMessage = "Título deve ser preenchido")]
        [Display(Name = "Título do Chamado")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Informe a descrição do chamado")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o setor")]
        public EnSetor Setor { get; set; }

        [Required(ErrorMessage = "Informe o Status")]
        public EnStatus Status { get; set; }

        public Guid IdUsuario { get; set; }
        public virtual UsuarioViewModel Usuario { get; set; }

        public SelectList ListaSetores { get; set; }
        public SelectList ListaStatus { get; set; }

        /// <summary>
        /// Carrega a lista de Setores a partir de um Enum
        /// </summary>
        /// <returns>Retorna um Select List com os Setores</returns>
        public SelectList CarregaListaSetores()
        {
            var listaSetores = new SelectList(Enum.GetValues(typeof(EnSetor)).Cast<EnSetor>().Select(c =>
                new SelectListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                }).ToList(),"Value","Text");

            return listaSetores;
        }

        /// <summary>
        /// Carrega a lista de Status a partir de um Enum
        /// </summary>
        /// <returns>Retorna um SelectList com os Status</returns>
        public SelectList CarregaListaStatus()
        {
            var listaStatus = new SelectList(Enum.GetValues(typeof(EnStatus)).Cast<EnStatus>().Select(c =>
                new SelectListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                }).ToList(), "Value", "Text");

            return listaStatus;
        }
    }
}