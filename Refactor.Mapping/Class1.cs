using AutoMapper;

namespace Refactor.Mapping
{
    public class Mapper
    {
        void Map<TSource, TTarget>(TSource source, TTarget target)
        {
            

            //Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDto>());
            ////or
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());
            //var mapper = config.CreateMapper();
            //// or
            //var mapper = new Mapper(config);
            //OrderDto dto = mapper.Map<OrderDto>(order);
            //// or
            //OrderDto dto = Mapper.Map<OrderDto>(order);
        }
    }
}
