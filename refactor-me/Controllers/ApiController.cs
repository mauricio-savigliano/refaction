using System.Net;
using System.Web.Http;
using Ninject;
using Refactor.Web.Common.Mappings;

namespace refactor_me.Controllers
{
    public abstract class BaseApiController : ApiController

    {
        [Inject]
        public IEntityMapper EntityMapper { protected get; set; }

        protected void ThrowNotFoundException()
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}