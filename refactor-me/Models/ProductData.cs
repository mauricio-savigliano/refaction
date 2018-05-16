using System;
using Refactor.Model;

namespace refactor_me.Models
{
    public class ProductData : IProductInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
}