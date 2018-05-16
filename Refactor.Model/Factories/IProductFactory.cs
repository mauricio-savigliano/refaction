using System;
using System.Dynamic;

namespace Refactor.Model.Factories
{
    public interface IProductFactory
    {
        Product Create(Guid id, IProductInfo info);
    }

    public interface IProductOptionFactory
    {
        ProductOption Create(Guid id, Guid productId, IProductOptionInfo info);
    }

    public class ProductFactory : IProductFactory
    {
        public Product Create(Guid id, IProductInfo info)
        {
            var product = new Product(id);
            product.Update(info);
            return product;
        }
    }

    public class ProductOptionFactory : IProductOptionFactory
    {
        public ProductOption Create(Guid id, Guid productId, IProductOptionInfo info)
        {
            var productOption = new ProductOption(id, productId);
            productOption.Update(info);
            return productOption;
        }
    }
}
