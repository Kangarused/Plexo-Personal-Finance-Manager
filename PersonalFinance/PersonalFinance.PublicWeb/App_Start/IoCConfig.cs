using Autofac;
using PersonalFinance.Common.Providers;
using PersonalFinance.Common.Providers.Logging;

namespace PersonalFinance.PublicWeb
{
    public class IoCConfig
    {
        public static void ConfigureIoc(ContainerBuilder builder)
        {
            builder.Register(c => new ConfigurationManagerProvider("")).As<IConfigurationManagerProvider>().SingleInstance();
            builder.Register(c => new LoggingProvider("Skilled Migration Public")).As<ILoggingProvider>().SingleInstance();
        }
    }
}