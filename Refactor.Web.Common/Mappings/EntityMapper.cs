using AutoMapper;

namespace Refactor.Web.Common.Mappings
{
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
