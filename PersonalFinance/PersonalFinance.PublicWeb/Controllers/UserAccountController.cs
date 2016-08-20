using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using PersonalFinance.PublicWeb.Models;
using PersonalFinance.PublicWeb.Models.Validators;
using PersonalFinance.PublicWeb.Providers;
using PersonalFinance.PublicWeb.Repositories;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;

namespace PersonalFinance.PublicWeb.Controllers
{
    public class UserAccountController : ApiController
    {
        private readonly ICryptoProvider _cryptoProvider;
        private readonly IAuthRepository _authRepository;

        public UserAccountController(
            IAuthRepository authRepository,
            ICryptoProvider cryptoProvider
            )
        {
            _authRepository = authRepository;
            _cryptoProvider = cryptoProvider;
        }

        [AllowAnonymous]
        [AcceptVerbs("POST")]
        public async Task<ActionResponse> Register(UserAccount model)
        {
            //validate Model
            var validator = new UserAccountValidator();
            var results = validator.Validate(model);

            if (!results.IsValid)
            {
                return ActionResponse.CreateWithErrors(results.Errors.Select(i => i.ErrorMessage));
            }

            if (await _authRepository.EmailExists(model.Email))
            {
                return ActionResponse.CreateWithErrors(new string[] { "That email is already registered in the system" });
            }

            var user = new User()
            {
                UserName = model.UserName,
                Name = model.Name,
                Email = model.Email,
                PasswordHash = model.Provider.IsNullOrWhiteSpace() ? _cryptoProvider.GetHash(model.Password) : null
            };

            ActionResponseGeneric<int> result = await _authRepository.AddUser(user);
            int newUserId = result.Response;

            model.Role = Role.PublicUser;
            var userRole = new UserRole()
            {
                UserId = newUserId,
                Role = model.Role.ToString()
            };

            await _authRepository.AddUserRole(userRole);

            return new ActionResponse
            {
                Succeed = true
            };
        }

        #region Helpers

        private string GetQueryString(HttpRequestMessage request, string key)
        {
            var queryStrings = request.GetQueryNameValuePairs();

            if (queryStrings == null) return null;

            var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

            if (string.IsNullOrEmpty(match.Value)) return null;

            return match.Value;
        }
        #endregion
    }
}