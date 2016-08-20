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
    public class AuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public IUserResolver UserResolver { get; set; }

        private readonly Role[] _roles;

        public AuthorizeAttribute()
        {
            _roles = new Role[0];
        }
       
        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles;
        }

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
           Func<Task<HttpResponseMessage>> continuation)
        {
            OnAuthentication(actionContext);
            return actionContext.Response ?? await continuation();
        }

        private void OnAuthentication(HttpActionContext actionContext)
        {
            UserDetails user = UserResolver.GetUser();

            var hasAccess =
                user.Roles.Contains(Role.SystemAdministrator) || 
                (user.IsPrivateApiClient && user.IsAnonymous && _roles.Length == 0) ||
                user.Roles.Intersect(_roles).Any();
            
            if (!hasAccess)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }
    }
}