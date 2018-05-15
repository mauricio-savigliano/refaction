using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Refactor.Persistance
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : Model
    {
        private readonly DbContext _persitanceContext;

        private DbSet<T> EntitySet => _persitanceContext.Set<T>();

        public EntityFrameworkRepository(DbContext persitanceContext)
        {
            _persitanceContext = persitanceContext;
        }

        public T GetById(Guid id)
        {
            return EntitySet.SingleOrDefault(x => x.Id == id);
        }

        public void Add(T entity)
        {
            EntitySet.Add(entity);
        }

        public void Remove(T entity)
        {
            EntitySet.Remove(entity);
        }

        public void SaveChanges()
        {
            _persitanceContext.SaveChanges();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return EntitySet.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return EntitySet.AsQueryable().GetEnumerator();
        }

        public Type ElementType => typeof(T);
        public Expression Expression => EntitySet.AsQueryable().Expression;
        public IQueryProvider Provider => EntitySet.AsQueryable().Provider;

        public void Dispose()
        {
            _persitanceContext?.Dispose();
        }
    }
}