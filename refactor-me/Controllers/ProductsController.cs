using System;
using System.Linq;
using System.Web.Http;
using refactor_me.Attributes;
using refactor_me.Models;
using Refactor.Model;
using Refactor.Persistance.Repositories;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : BaseApiController
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<ProductOption> _productOptions;
            
        public ProductsController(
            IRepository<Product> products, 
            IRepository<ProductOption> productOptions)
        {
            _products = products;
            _productOptions = productOptions;
        }

        [Route]
        [HttpGet]
        public ListWrapper<ProductData> GetAll()
        {
            var products = _products.ToList().Select(MapToProductData);
            return new ListWrapper<ProductData>(products);
        }

        [Route]
        [HttpGet]
        public ListWrapper<ProductData> SearchByName(string name)
        {
            var filteredProducts = _products.Where(p => p.Name.Contains(name))
                .ToList()
                .Select(MapToProductData);

            return new ListWrapper<ProductData>(filteredProducts);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            return TryGetProduct(id);
        }

        [Route]
        [HttpPost, UnitOfWork]
        [ValidateModel]
        public void Create(ProductData product)
        {
            var newProduct = new Product(product.Id);
            newProduct.Update(product);
            _products.Add(newProduct);
        }

        [Route("{id}")]
        [HttpPut, UnitOfWork]
        [ValidateModel]
        public void Update(Guid id, ProductData product)
        {
            var productToUpdate = TryGetProduct(id);            
            productToUpdate.Update(product);
        }

        [Route("{id}")]
        [HttpDelete, UnitOfWork]
        public void Delete(Guid id)
        {
            var productToRemove = TryGetProduct(id);
            _products.Remove(productToRemove);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ListWrapper<ProductOptionData> GetOptions(Guid productId)
        {
            var productOptions = _productOptions.Where(po => po.ProductId == productId)
                .ToList()
                .Select(MapToProductOptionData);

            return new ListWrapper<ProductOptionData>(productOptions);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOptionData GetOption(Guid productId, Guid id)
        {
            return MapToProductOptionData(TryGetProductOption(productId, id));
        }

        [Route("{productId}/options")]
        [HttpPost, UnitOfWork]
        [ValidateModel]
        public void CreateOption(Guid productId, ProductOptionData option)
        {
            var newProductOption = new ProductOption(option.Id, productId);
            newProductOption.Update(option);
            _productOptions.Add(newProductOption);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut, UnitOfWork]
        [ValidateModel]
        public void UpdateOption(Guid productId, Guid id, ProductOptionData option)
        {
            var optionToUpdate = TryGetProductOption(productId, id);
            optionToUpdate.Update(option);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete, UnitOfWork]
        public void DeleteOption(Guid productId, Guid id)
        {
            var productOptionToDelete = TryGetProductOption(productId, id);
            _productOptions.Remove(productOptionToDelete);
        }

        private Product TryGetProduct(Guid id)
        {
            var product = _products.GetById(id);

            if (product == null)
                ThrowNotFoundException();

            return product;
        }

        private ProductOption TryGetProductOption(Guid productId, Guid id)
        {
            var productOption = _productOptions.SingleOrDefault(po => po.ProductId == productId && po.Id == id);

            if (productOption == null)
                ThrowNotFoundException();

            return productOption;
        }

        private ProductData MapToProductData(Product product)
        {
            return EntityMapper.Map<Product, ProductData>(product);
        }

        private ProductOptionData MapToProductOptionData(ProductOption productOption)
        {
            return EntityMapper.Map<ProductOption, ProductOptionData>(productOption);
        }

        protected override void Dispose(bool disposing)
        {
            // No UOW is implemented, disposing manually.
            _products?.Dispose();
            _productOptions?.Dispose();

            base.Dispose(disposing);
        }
    }
}
