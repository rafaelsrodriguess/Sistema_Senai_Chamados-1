using Senai.Chamados.Domain.Enum;
using Senai.Chamados.Web.ViewModels.Usuario;
using System;
using System.Web.Mvc;

namespace Senai.Chamados.Web.ViewModels.Chamado
{
    public class ChamadoViewModel : BaseViewModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public EnSetor Setor { get; set; }
        public EnStatus Status { get; set; }
        public Guid IdUsuario { get; set; }
        public virtual UsuarioViewModel Usuario { get; set; }

        public SelectList ListaSetores { get; set; }
        public SelectList ListaStatus { get; set; }

    }
}