using System;
using System.ComponentModel.DataAnnotations;
using Refactor.Model;

namespace refactor_me.Models
{
    public class ProductData : IProductInfo
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal DeliveryPrice { get; set; }
    }
}