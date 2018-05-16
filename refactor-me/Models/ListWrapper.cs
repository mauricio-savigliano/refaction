using System.Collections.Generic;

namespace refactor_me.Models
{
    public class ListWrapper<T>
    {
        public IEnumerable<T> Items { get; }

        public ListWrapper(IEnumerable<T> items)
        {
            Items = items;

        }
    }
}