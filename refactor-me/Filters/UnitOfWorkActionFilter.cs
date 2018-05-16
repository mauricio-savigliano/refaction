using System.Net.Http;
using System.Web.Http.Filters;
using Refactor.Persistance;

namespace refactor_me.Filters
{
    public class UnitOfWorkActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var unitOfWork = actionExecutedContext.Request.GetDependencyScope().GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            if (actionExecutedContext.Exception == null)
            {
                unitOfWork?.Save();
            }
        }
    }
}