using System;

namespace Refactor.Model
{
    public interface IProductOptionInfo
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    public class ProductOption : Persistance.Entities.Model
    {
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        protected ProductOption() { }

        public ProductOption(Guid id, Guid productId)
        {
            Id = id;
            ProductId = productId;
        }

        public void Update(IProductOptionInfo info)
        {
            Name = info.Name;
            Description = info.Description;
        }
    }
}