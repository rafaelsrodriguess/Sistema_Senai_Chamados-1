using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Senai.Chamados.Data.Contexto;
using Senai.Chamados.Domain.Contratos;
using Senai.Chamados.Domain.Entidades;

namespace Senai.Chamados.Data.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly SenaiChamadosDbContext _contexto;

        public UsuarioRepositorio()
        {
            _contexto = new SenaiChamadosDbContext();
        }

        /// <summary>
        /// Altera um usuário no banco
        /// </summary>
        /// <param name="domain">Dados do usuário</param>
        /// <returns>Retorna true para usuário cadastrado e false caso não seja cadastrado</returns>
        public bool Alterar(UsuarioDomain domain)
        {
            _contexto.Entry<UsuarioDomain>(domain).State = System.Data.Entity.EntityState.Modified;
            int linhasAlteradas = _contexto.SaveChanges();

            if (linhasAlteradas > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Busca o usuário pelo Id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="includes">Dominios para fazer Inner Join</param>
        /// <returns></returns>
        public UsuarioDomain BuscarPorId(Guid id, string[] includes = null)
        {
            var query = _contexto.Usuarios.AsQueryable();

            if(includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Exclui um usuário do banco
        /// </summary>
        /// <param name="domain">Dados do usuário</param>
        /// <returns>Retorna true para usuário cadastrado e false caso não seja cadastrado</returns>
        public bool Deletar(UsuarioDomain domain)
        {
            _contexto.Usuarios.Remove(domain);
            int linhasExcluidas = _contexto.SaveChanges();

            if (linhasExcluidas > 0)
                return true;
            else
                return false;
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }

        /// <summary>
        /// Insere um novo usuário no banco
        /// </summary>
        /// <param name="domain">Dados do Usuario</param>
        /// <returns>Retorna true para usuário cadastrado e false caso não seja cadastrado</returns>
        public bool Inserir(UsuarioDomain domain)
        {
            _contexto.Usuarios.Add(domain);
            int linhasIncluidas = _contexto.SaveChanges();

            if (linhasIncluidas > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Lista todos os usuários do Banco
        /// </summary>
        /// <param name="includes">Dominios para fazer Inner Join</param>
        /// <returns>Retorna uma lista de usuário domain</returns>
        public List<UsuarioDomain> Listar(string[] includes = null)
        {
            var query = _contexto.Usuarios.AsQueryable();

            if(includes != null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }

                //query = includes.Aggregate(query, (current, include) => current.Include(include)); 
            }

            return query.ToList();
        }

        /// <summary>
        /// Valida um usuário no banco
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <param name="senha">Senha do Usuário</param>
        /// <returns>Retorna um Usuário caso o mesmo seja válido</returns>
        public UsuarioDomain Login(string email, string senha)
        {
            UsuarioDomain objUsuario = _contexto.Usuarios.FirstOrDefault(x => x.Email == email && x.Senha == senha);

            if (objUsuario == null)
                return null;
            else
                return objUsuario;
        }
    }
}
