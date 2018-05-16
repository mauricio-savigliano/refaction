using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace refactor_me.Filters
{
    public class DuplicatedEntityKeyActionFIlter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;
            var innerException = exception.InnerException?.InnerException as SqlException;

            if (exception is DbUpdateException && innerException != null && innerException.Number == 2627)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    ReasonPhrase = "Duplicated key"
                };
            }
        }
    }
}