using System.Data.Entity.ModelConfiguration;
using Refactor.Persistance.Entities;

namespace Refactor.Persistance.Mappings
{
    public class ModelMapping<T> : EntityTypeConfiguration<T> where T : Model
    {
        public ModelMapping()
        {
            HasKey(x => x.Id);
        }
    }
}