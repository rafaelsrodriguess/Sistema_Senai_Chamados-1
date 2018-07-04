using System;
using System.Collections.Generic;
using Senai.Chamados.Data.Contexto;
using Senai.Chamados.Domain.Contratos;
using Senai.Chamados.Domain.Entidades;

namespace Senai.Chamados.Data.Repositorios
{
    public class ChamadoRepositorio : IChamadoRepositorio
    {
        private readonly SenaiChamadosDbContext _contexto;

        public ChamadoRepositorio()
        {
            _contexto = new SenaiChamadosDbContext();
        }

        public bool Alterar(ChamadoDomain domain)
        {

            _contexto.Entry<ChamadoDomain>(domain).State = System.Data.Entity.EntityState.Modified;
            int linhasAlteradas = _contexto.SaveChanges();

            if (linhasAlteradas > 0)
                return true;
            else
                return false;
        }

        public ChamadoDomain BuscarPorId(Guid id, string[] includes = null)
        {
            throw new NotImplementedException();
        }

        public bool Deletar(ChamadoDomain domain)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Inserir(ChamadoDomain domain)
        {
            throw new NotImplementedException();
        }

        public List<ChamadoDomain> Listar(Guid idUsuario, string[] includes = null)
        {
            throw new NotImplementedException();
        }

        public List<ChamadoDomain> Listar(string[] includes = null)
        {
            throw new NotImplementedException();
        }
    }
}
