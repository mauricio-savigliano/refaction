using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using Refactor.Model;
using Refactor.Persistance;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<ProductOption> _productOptions;

        protected ProductsController() { }
            
        public ProductsController(IRepository<Product> products, IRepository<ProductOption> productOptions)
        {
            _products = products;
            _productOptions = productOptions;
        }

        [Route]
        [HttpGet]
        public ProductsData GetAll()
        {
            return new ProductsData(_products.ToList());
        }

        [Route]
        [HttpGet]
        public ProductsData SearchByName(string name)
        {
            var products = _products.Where(p => p.Name.CompareTo(name,) > 0);

            return new ProductsData(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            var product = _products.GetById(id);

            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            _products.Add(product);
            _products.SaveChanges();
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, ProductData product)
        {
            var productToUpdate = _products.GetById(product.Id);

            if (productToUpdate == null)
            {
                productToUpdate = new Product();
                _products.Add(productToUpdate);
            }

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            productToUpdate.DeliveryPrice = product.DeliveryPrice;

            _products.SaveChanges();
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            var product = _products.GetById(id);

            if (product != null)
            {
                _products.Remove(product);
                _products.SaveChanges();
            }
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptionsData GetOptions(Guid productId)
        {
            var productOptions = _productOptions.Where(po => po.ProductId == productId);
            return new ProductOptionsData(productOptions);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = _productOptions.SingleOrDefault(p => p.Id == id && p.ProductId == productId);

            if (option == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            var productOption = _productOptions.GetById();

            //option.ProductId = productId;
            //option.Save();
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            var optionToUpdate = _productOptions.GetById(id);

            if (optionToUpdate == null) return;

            optionToUpdate.Description = option.Description;
            optionToUpdate.Name = option.Name;

            _productOptions.SaveChanges();
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            var productOptionToDelete = _productOptions.GetById(id);
            
            if (productOptionToDelete != null)
                _productOptions.Remove(productOptionToDelete);
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
