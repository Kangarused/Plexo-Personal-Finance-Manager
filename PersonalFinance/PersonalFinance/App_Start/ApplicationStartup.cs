using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PersonalFinance.Providers;
using PersonalFinance.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

[assembly: OwinStartup(typeof(PersonalFinance.ApplicationStartup))]
namespace PersonalFinance
{
    public class ApplicationStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            app.CreatePerOwinContext<SqlConnection>(() => new SqlConnection(connectionString));
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<AccountManager>(AccountManager.Create);

            ConfigureOAuth(app);

            ConfigureWebAPI(app);

        }

        void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        void ConfigureWebAPI(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config); // Register the Routes
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}