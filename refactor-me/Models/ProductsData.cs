using System.Collections.Generic;

namespace refactor_me.Models
{
    public class ProductsData
    {
        public List<ProductData> Items { get; private set; }

        public ProductsData(List<ProductData> items)
        {
            Items = items;
        }

        //public Products(string name)
        //{
        //    LoadProducts($"where lower(name) like '%{name.ToLower()}%'");
        //}
    }
}