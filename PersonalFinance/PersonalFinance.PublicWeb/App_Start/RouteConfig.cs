using System.Web.Mvc;
using System.Web.Routing;

namespace PersonalFinance.PublicWeb
{
    public class PublicRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("AuthCompleted", "AuthCompleted", new { controller = "Home", action = "AuthCompleted" });
        }
    }
}