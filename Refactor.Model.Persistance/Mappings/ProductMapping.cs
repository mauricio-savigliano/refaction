using Refactor.Persistance;
using Refactor.Persistance.Mappings;

namespace Refactor.Model.Persistance.Mappings
{
    public class ProductMapping : ModelMapping<Product>
    {
        public ProductMapping()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            Property(x => x.Description).HasMaxLength(500);
            Property(x => x.Price).IsRequired();
            Property(x => x.DeliveryPrice).IsRequired();
        }
    }
}
