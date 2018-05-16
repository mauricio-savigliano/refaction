using System;
using System.ComponentModel.DataAnnotations;
using Refactor.Model;

namespace refactor_me.Models
{
    public class ProductOptionData : IProductOptionInfo
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}