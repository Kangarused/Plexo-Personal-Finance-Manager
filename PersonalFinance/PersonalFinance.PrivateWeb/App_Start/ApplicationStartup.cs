using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using PersonalFinance.Common;
using PersonalFinance.Common.Providers;
using PersonalFinance.PrivateWeb;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ApplicationStartup))]
namespace PersonalFinance.PrivateWeb
{
    public class ApplicationStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            AreaRegistration.RegisterAllAreas();

            var webAssembly = typeof(ApplicationStartup).Assembly;
            var builder = CommonIoCConfig.InitIoc(webAssembly, new[] { typeof(CommonIoCConfig).Assembly });
            IoCConfig.ConfigureIoc(builder);
            var container = CommonIoCConfig.WireIoc(builder, config, webAssembly);

            var configManager = container.Resolve<IConfigurationManagerProvider>();
            CommonWebApiConfig.Register(config, configManager.IsDebugMode());

            WebApiConfig.Register(config, container);

            app.UseWebApi(config);
        }   
    }
}