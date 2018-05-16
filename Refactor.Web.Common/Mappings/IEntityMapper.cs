namespace Refactor.Web.Common.Mappings
{
    public interface IEntityMapper
    {
        TTarget Map<TSource, TTarget>(TSource source);
    }
}