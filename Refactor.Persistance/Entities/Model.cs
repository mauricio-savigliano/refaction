using System;

namespace Refactor.Persistance.Entities
{
    public abstract class Model
    {
        public Guid Id { get; set; }

        protected Model() : this(Guid.NewGuid()) { }

        protected Model(Guid id)
        {
            Id = id;
        }
    }
}
