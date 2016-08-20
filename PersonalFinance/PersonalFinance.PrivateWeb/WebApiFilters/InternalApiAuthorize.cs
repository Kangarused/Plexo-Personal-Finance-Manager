using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using PersonalFinance.Common.Model;
using PersonalFinance.PrivateWeb.Models;
using PersonalFinance.PrivateWeb.Providers;

namespace PersonalFinance.PrivateWeb.WebApiFilters
{
    public class InternalApiAuthorize : FilterAttribute, IAuthorizationFilter
    {
        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
           Func<Task<HttpResponseMessage>> continuation)
        {
            OnAuthentication(actionContext);
            return actionContext.Response ?? await continuation();
        }

        private void OnAuthentication(HttpActionContext actionContext)
        {
            if (
                actionContext.Request.Headers.UserAgent.Count==0 || 
                actionContext.Request.Headers.UserAgent.ElementAt(0).Product.Name != Constants.PrivateApiUserAgent)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }
    }
}