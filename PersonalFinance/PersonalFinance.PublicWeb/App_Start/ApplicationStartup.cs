using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using PersonalFinance.Common;
using PersonalFinance.Common.Providers;
using PersonalFinance.PublicWeb;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ApplicationStartup))]
namespace PersonalFinance.PublicWeb
{
    public class ApplicationStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            AreaRegistration.RegisterAllAreas();
            log4net.Config.XmlConfigurator.Configure();

            var webAssembly = typeof(ApplicationStartup).Assembly;
            var builder = CommonIoCConfig.InitIoc(webAssembly, new []{ typeof(CommonIoCConfig).Assembly });
            IoCConfig.ConfigureIoc(builder);
            var container = CommonIoCConfig.WireIoc(builder, config, webAssembly);

            var configManager = container.Resolve<IConfigurationManagerProvider>();
            CommonWebApiConfig.Register(config, configManager.IsDebugMode());

            ConfigureOWinPipeline.Configure(app, container, config);

            app.UseWebApi(config);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CommonRouteConfig.RegisterRoutes(RouteTable.Routes);
            PublicRouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}