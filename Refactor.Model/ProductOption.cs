using System;

namespace Refactor.Model
{
    public class ProductOption : Persistance.Model
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}