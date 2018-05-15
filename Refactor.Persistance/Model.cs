using System;

namespace Refactor.Persistance
{
    public abstract class Model
    {
        public Guid Id { get; set; }

        protected Model()
        {
            Id = Guid.NewGuid();
        }
    }
}
