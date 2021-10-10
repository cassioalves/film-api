using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Film.Entity
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly FilmContext dbContext;
        public Repository(FilmContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public virtual IQueryable<T> RetornarTodos(params Expression<Func<T, object>>[] includes)
        {
            //return dbContext.Set<T>();
            return includes.Aggregate(dbContext.Set<T>().AsQueryable(), (current, includeProperty) => current.Include(includeProperty));
        }

        public virtual IQueryable<T> Consultar(Expression<Func<T, bool>> expressao)
        {
            return dbContext.Set<T>().Where(expressao);
        }

        public virtual T Retornar(long id)
        {
            return Consultar(e => e.Id == id).FirstOrDefault();
        }

        public virtual T Alterar(T entidade)
        {
            try
            {
                var retorno = dbContext.Update(entidade).Entity;
                dbContext.SaveChanges();
                return retorno;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public virtual T Incluir(T entidade)
        {
            try
            {
                var retorno = dbContext.Add(entidade);
                dbContext.SaveChanges();
                retorno.Reload();
                return retorno.Entity;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public virtual void Deletar(long id)
        {
            try
            {
                var entidadeADeletar = Retornar(id);
                if (entidadeADeletar != null)
                {
                    dbContext.Remove(entidadeADeletar);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}