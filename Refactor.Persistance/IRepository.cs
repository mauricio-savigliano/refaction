using System;
using System.Linq;

namespace Refactor.Persistance
{
    public interface IRepository<T> : IQueryable<T>, IDisposable where T : Model
    {
        T GetById(Guid id);
        void Add(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
