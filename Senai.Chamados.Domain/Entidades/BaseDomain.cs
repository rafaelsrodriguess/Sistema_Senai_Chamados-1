using System;
using System.ComponentModel.DataAnnotations;

namespace Senai.Chamados.Domain.Entidades
{
    /// <summary>
    /// Doninio base do sistema
    /// </summary>
    public class BaseDomain
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
