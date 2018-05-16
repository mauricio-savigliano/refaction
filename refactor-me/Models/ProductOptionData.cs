using System;
using Refactor.Model;

namespace refactor_me.Models
{
    public class ProductOptionData : IProductOptionInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}