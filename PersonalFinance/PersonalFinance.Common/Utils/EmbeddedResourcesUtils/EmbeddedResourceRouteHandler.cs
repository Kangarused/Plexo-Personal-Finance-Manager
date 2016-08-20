using System.Web;
using System.Web.Routing;

namespace PersonalFinance.Common.Utils.EmbeddedResourcesUtils
{
    public class EmbeddedResourceRouteHandler : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new EmbeddedResourceHttpHandler(requestContext.RouteData);
        }
    }
}
