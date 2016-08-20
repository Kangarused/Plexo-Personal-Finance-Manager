using System;
using System.Threading.Tasks;
using DOB.SkilledMigration.Common.Model;
using DOB.SkilledMigration.PublicWeb.Repositories;
using DOB.SkilledMigration.PublicWeb.Utils;
using Microsoft.Owin.Security.Infrastructure;

namespace DOB.SkilledMigration.PublicWeb.Providers
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly Func<IAuthRepository> _userServiceFactory;
        private IAuthRepository AuthRepository
        {
            get { return _userServiceFactory.Invoke(); }
        }

        public SimpleRefreshTokenProvider(Func<IAuthRepository> userServiceFactory)
        {
            _userServiceFactory = userServiceFactory;
        }


        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");



            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshToken()
            {
                TokenUid = CryptoUtils.GetHash(refreshTokenId),
                ClientId = clientid,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var result = await AuthRepository.AddRefreshToken(token);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }


        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = CryptoUtils.GetHash(context.Token);

           
                RefreshToken refreshToken = await AuthRepository.FindRefreshToken(hashedTokenId);

                if (refreshToken != null )
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                    bool result = await AuthRepository.RemoveRefreshToken(hashedTokenId);
                }
            
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}