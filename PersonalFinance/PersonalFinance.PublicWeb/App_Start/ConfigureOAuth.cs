using System;
using System.Web.Http;
using Autofac;
using PersonalFinance.Common.Providers;
using PersonalFinance.PublicWeb.Providers;
using PersonalFinance.PublicWeb.Repositories;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace PersonalFinance.PublicWeb
{
    public static class ConfigureOWinPipeline
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public static void Configure(IAppBuilder app, IContainer container, HttpConfiguration config)
        {
            Func<IAuthRepository> userServiceFactory = container.Resolve<IAuthRepository>;
            Func<ICryptoProvider> cryptoServiceFactory = container.Resolve<ICryptoProvider>;

            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(userServiceFactory, cryptoServiceFactory)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}