using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Utils.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Facebook;

namespace PersonalFinance.PublicWeb.Providers
{
    public interface IInternalApiCallerResolverProvider
    {
        InternalApiCallerIdentity Caller { get; }
    }

    [PerRequest]
    public class InternalApiCallerResolverProvider : IInternalApiCallerResolverProvider
    {
        public InternalApiCallerIdentity Caller { get; }

        public InternalApiCallerResolverProvider()
        {
            Caller = GetCaller();
        }

        private InternalApiCallerIdentity GetCaller()
        {
            var context = System.Web.HttpContext.Current;
            if (context == null) return null;

            var request = context.Request;
            string ipStr = request.GetIpAddress();
            int id = 0;
            string email = "Anonymous";
            string name = "Anonymous";
            string role = Role.AnonymousOverPrivateApi.ToString();
            bool isAnonymous = true;

            if (context.User!=null && context.User.Identity.IsAuthenticated)
            {

                var claimsIdentity = (ClaimsIdentity) context.User.Identity;
                var claims = claimsIdentity.Claims.ToList();
                var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                role = roleClaim == null ? null : roleClaim.Value;
                email = context.User.Identity.Name;
                isAnonymous = false;
                name = claims.First(c => c.Type == ClaimTypes.GivenName).Value;
                id = Convert.ToInt32(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }

            return new InternalApiCallerIdentity { Email = email, IpAddress = ipStr, Role = role, IsAnonymous = isAnonymous, Name = name, Id = id };
        }
    }
}