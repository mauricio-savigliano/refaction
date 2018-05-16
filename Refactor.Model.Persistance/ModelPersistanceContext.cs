using System.Data.Common;
using System.Data.Entity;
using System.Reflection;

namespace Refactor.Model.Persistance
{
    public class ModelPersistanceContext : DbContext
    {
        public DbSet<Product> Products { get; }
        public DbSet<ProductOption> ProductOptions { get; }

        public ModelPersistanceContext(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
