using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using refactor_me.Attributes;
using refactor_me.Models;
using Refactor.Mapping;
using Refactor.Model;
using Refactor.Model.Factories;
using Refactor.Persistance;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : BaseApiController
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<ProductOption> _productOptions;
        private readonly IProductFactory _productFactory;
        private readonly IProductOptionFactory _productOptionFactory;

        protected ProductsController(IProductFactory productFactory, IProductOptionFactory productOptionFactory)
        {
            _productFactory = productFactory;
            _productOptionFactory = productOptionFactory;
        }
            
        public ProductsController(
            IRepository<Product> products, 
            IRepository<ProductOption> productOptions, 
            IProductFactory productFactory, 
            IProductOptionFactory productOptionFactory)
        {
            _products = products;
            _productOptions = productOptions;
            _productFactory = productFactory;
            _productOptionFactory = productOptionFactory;
        }

        [Route]
        [HttpGet]
        public ListWrapper<ProductData> GetAll()
        {
            var products = _products.ToList();
            
            return new ListWrapper<ProductData>(products.Select(EntityMapper.Map<Product, ProductData>));
        }

        [Route]
        [HttpGet]
        public ListWrapper<ProductData> SearchByName(string name)
        {
            var filteredProducts = _products.Where(p => p.Name.Contains(name)).ToList();
            
            return new ListWrapper<ProductData>(filteredProducts.Select(EntityMapper.Map<Product, ProductData>));
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
        [ValidateModel]
        public void Create(ProductData product)
        {
            var productToCreate = _productFactory.Create(product.Id, product);
            _products.Add(productToCreate);
            _products.SaveChanges();
        }

        [Route("{id}")]
        [HttpPut]
        [ValidateModel]
        public void Update(Guid id, ProductData product)
        {
            var productToUpdate = _products.GetById(product.Id);

            if (productToUpdate == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
            productToUpdate.Update(product);

            _products.SaveChanges();
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            var product = _products.GetById(id);

            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _products.Remove(product);
            _products.SaveChanges();
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ListWrapper<ProductOptionData> GetOptions(Guid productId)
        {
            var productOptions = _productOptions.Where(po => po.ProductId == productId).ToList();
            return new ListWrapper<ProductOptionData>(productOptions.Select(EntityMapper.Map<ProductOption, ProductOptionData>));
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
        [ValidateModel]
        public void CreateOption(Guid productId, ProductOptionData option)
        {
            var newProductOption = _productOptionFactory.Create(option.Id, option.ProductId, option);
            _productOptions.Add(newProductOption);
            _productOptions.SaveChanges();
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        [ValidateModel]
        public void UpdateOption(Guid id, ProductOptionData option)
        {
            var optionToUpdate = _productOptions.GetById(id);

            if (optionToUpdate == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            optionToUpdate.Update(option);

            _productOptions.SaveChanges();
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            var productOptionToDelete = _productOptions.GetById(id);
            
            if (productOptionToDelete == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

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
