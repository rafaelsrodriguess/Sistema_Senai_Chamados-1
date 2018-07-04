using Senai.Chamados.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Senai.Chamados.Domain.Entidades
{
    /// <summary>
    /// Classe Reponsavel pela entidade Chamados
    /// </summary>
    [Table("Chamados")]
    public class ChamadoDomain : BaseDomain
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public EnSetor Setor { get; set; }
        public EnStatus Status { get; set; }

        [ForeignKey("Usuario")]
        public Guid IdUsuario { get; set; }

        public virtual UsuarioDomain Usuario { get; set; }
    }
}
