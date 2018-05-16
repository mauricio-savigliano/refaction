using System;

namespace Refactor.Model
{
    public interface IProductInfo
    {
        string Name { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        decimal DeliveryPrice { get; set; }
    }

    public class Product : Persistance.Model
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public decimal DeliveryPrice { get; private set; }

        protected Product(){ }

        public Product(Guid id)
        {
            Id = id;
        }

        public void Update(IProductInfo info)
        {
            Name = info.Name;
            Description = info.Description;
            Price = info.Price;
            DeliveryPrice = info.DeliveryPrice;
        }
    }
}
