using System;

namespace Senai.Chamados.Web.ViewModels
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}