using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ServerDocFabricator.DAL
{
    public interface IRepository<T> where T : class, new()
    {
        T Add(T entity);

        T Find(Guid id);
        T Find(Expression<Func<T, bool>> predic);
        List<T> FindAll(Expression<Func<T, bool>> predic);

        void Update(T entity);
        bool Remove(T entity);
        bool Remove(Guid entity);

        int Count();

    }
}
