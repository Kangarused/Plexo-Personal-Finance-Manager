using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using PersonalFinance.PublicWeb.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace PersonalFinance.PublicWeb.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private Func<IAuthRepository> _autRepositoryFunc;
        private IAuthRepository AuthRepository
        {
            get { return _autRepositoryFunc.Invoke(); } //Per instance 
        }
        
        private ICryptoProvider _cryptoProvider;

        public SimpleAuthorizationServerProvider(Func<IAuthRepository> authRepositoryFactory, Func<ICryptoProvider> cryptoFactory)
        {
            this._autRepositoryFunc = authRepositoryFactory;
            this._cryptoProvider = cryptoFactory.Invoke(); //Singleton
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            AuthClient client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return;
            }

           
            client = await AuthRepository.GetAuthClient(context.ClientId);
            

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return;
            }
            
            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return;
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);

            context.Validated();
            return;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            User user;

            //If username is email check against emails, else check against usernames
            if (Regex.IsMatch(context.UserName, Common.Model.Constants.EmailVerificationRegex))
            {
                user = await AuthRepository.GetUserByEmail(context.UserName);
            }
            else
            {
                user = await AuthRepository.GetUserByUsername(context.UserName);
            }

            if (user.PasswordHash == null || context.Password == null)
            {
                context.SetError("invalid_grant", "Password is required");
                return;
            }

            if (user == null || !_cryptoProvider.VerifyHash(user.PasswordHash, context.Password)    )
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            if (user.UserRoles == null || user.UserRoles.Count==0)
            {
                context.SetError("invalid_grant", "The user does not have any role.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            string roles = string.Join(",", user.UserRoles.Select(i=>i.Role));
            identity.AddClaim(new Claim(ClaimTypes.Role, roles));
            

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    { 
                        "userName", context.UserName
                    },
                    {
                        "role", roles
                    },
                    {
                        "name", user.Name
                    },
                     {
                        "id", user.Id.ToString()
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            
            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

    }
}