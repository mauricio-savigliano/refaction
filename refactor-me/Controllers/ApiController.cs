using System.Web.Http;
using Ninject;
using Refactor.Mapping;

namespace refactor_me.Controllers
{
    public abstract class BaseApiController : ApiController

    {
        [Inject]
        public IEntityMapper EntityMapper { protected get; set; }
    }
}