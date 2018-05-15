using System.Collections.Generic;
using Refactor.Model;

namespace refactor_me.Models
{
    public class ProductOptionsData
    {
        public IEnumerable<ProductOptionData> Items { get; }

        public ProductOptionsData(IEnumerable<ProductOptionData> items)
        {
            Items = items;
        }
    }
}