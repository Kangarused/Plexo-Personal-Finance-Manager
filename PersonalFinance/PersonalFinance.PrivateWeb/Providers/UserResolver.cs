using System;
using System.Collections.Generic;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Utils.Extensions;
using PersonalFinance.PrivateWeb.Models;

namespace PersonalFinance.PrivateWeb.Providers
{
    public interface IUserResolver
    {
        UserDetails GetUser();
    }

    [PerRequest]
    public class UserResolver : IUserResolver
    {
        private readonly IHttpContextProvider _httpContextProvider;

        public UserResolver(
            IHttpContextProvider httpContextProvider
            )
        {
            _httpContextProvider = httpContextProvider;
        }

        public UserDetails GetUser()
        {
            if (_httpContextProvider.IsInternalApiCall)
            {
                var caller = _httpContextProvider.GetInternalApiCallerIdentity();
                return new UserDetails
                {
                    IsAnonymous = caller.IsAnonymous,
                    DisplayName = caller.Name,
                    Email = caller.Email,
                    IsPrivateApiClient = true,
                    Roles = new List<Role> {{caller.Role.EnumParse<Role>()}},
                    IpAddress = caller.IpAddress,
                    Id = caller.Id
                };
            }
            throw new UnauthorizedAccessException("Attempting access through the private side is not allowed");
        }
    }
}
