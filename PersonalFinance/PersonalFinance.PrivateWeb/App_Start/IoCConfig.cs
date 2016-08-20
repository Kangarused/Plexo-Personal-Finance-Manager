using System.Net.Http.Formatting;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using PersonalFinance.Common.Providers;
using PersonalFinance.Common.Providers.Logging;
using PersonalFinance.PrivateWeb.Controllers;
using PersonalFinance.PrivateWeb.Database.OrmLiteInfrastructure;
using PersonalFinance.PrivateWeb.WebApiFilters;

namespace PersonalFinance.PrivateWeb
{
    public class IoCConfig
    {
        public static void ConfigureIoc(ContainerBuilder builder)
        {
            builder.Register(c => new ConfigurationManagerProvider("PersonalFinanceConnectionString")).As<IConfigurationManagerProvider>().SingleInstance();

            builder.Register(c => new LoggingProvider("Personal Finance Private")).As<ILoggingProvider>().SingleInstance();


            builder.Register(c => new WebApiDecodeFilter(c.Resolve<ICryptoProvider>()))
            .AsWebApiActionFilterFor<ApiController>()
            .InstancePerRequest();

            builder.Register(c => new TransactionFilterAttribute(c.Resolve<IUnitOfWork>()))
            .AsWebApiActionFilterFor<ApiController>()
            .InstancePerRequest();

            builder.Register(c => new WebApiExceptionFilter())
            .AsWebApiExceptionFilterFor<ApiController>()
            .InstancePerRequest();

        }
    }
}