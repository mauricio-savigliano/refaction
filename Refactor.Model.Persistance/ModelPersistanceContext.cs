using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace Refactor.Model.Persistance
{
    public class ModelPersistanceContext : DbContext
    {
        public DbSet<Product> Products { get; }
        public DbSet<ProductOption> ProductOptions { get; }

        public ModelPersistanceContext(string connectionString) : base(connectionString) { }

        public ModelPersistanceContext()
        {
            // Avoid checking for model metadata.
            Database.SetInitializer<ModelPersistanceContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(GetType()));
            base.OnModelCreating(modelBuilder);
        }
    }
}
