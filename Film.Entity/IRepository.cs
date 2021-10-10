using System;
using System.Linq;
using System.Linq.Expressions;

namespace Film.Entity
{
    public interface IRepository<T>
    {
        IQueryable<T> RetornarTodos(params Expression<Func<T, object>>[] includes);
        IQueryable<T> Consultar(Expression<Func<T, bool>> expressao);
        T Retornar(long id);
        T Alterar(T entidade);
        T Incluir(T entidade);
        void Deletar(long id);
    }
}
