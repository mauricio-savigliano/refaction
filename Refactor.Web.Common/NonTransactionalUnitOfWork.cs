using System.Data.Entity;
using Refactor.Persistance;

namespace Refactor.Web.Common
{
    public class NonTransactionalUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public NonTransactionalUnitOfWork(DbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
