using System.Data.Entity.ModelConfiguration;

namespace Refactor.Persistance
{
    public class ModelMapping<T> : EntityTypeConfiguration<T> where T : Persistance.Model
    {
        public ModelMapping()
        {
            HasKey(x => x.Id);
        }
    }
}