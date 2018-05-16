using AutoMapper;

namespace Refactor.Mapping
{
    public interface IEntityMapper
    {
        TTarget Map<TSource, TTarget>(TSource source);
    }

    public class EntityMapper : IEntityMapper
    {
        public TTarget Map<TSource, TTarget>(TSource source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TTarget>());
            var mapper = config.CreateMapper();
            
            return mapper.Map<TTarget>(source);
        }
    }
}
