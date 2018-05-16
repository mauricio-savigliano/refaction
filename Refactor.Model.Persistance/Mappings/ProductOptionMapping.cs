using Refactor.Persistance;
using Refactor.Persistance.Mappings;

namespace Refactor.Model.Persistance.Mappings
{
    public class ProductOptionMapping : ModelMapping<ProductOption>
    {
        public ProductOptionMapping()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            Property(x => x.Description).HasMaxLength(500);
        }  
    }
}
