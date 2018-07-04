using Senai.Chamados.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace Senai.Chamados.Domain.Contratos
{
    public interface IChamadoRepositorio : IDisposable, IRepositorioBase<ChamadoDomain>
    {
        List<ChamadoDomain> Listar(Guid idUsuario, string[] includes = null);
    }
}
