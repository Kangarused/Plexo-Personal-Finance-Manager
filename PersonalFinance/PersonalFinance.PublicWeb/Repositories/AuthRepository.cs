using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Model;
using PersonalFinance.PublicWeb.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace PersonalFinance.PublicWeb.Repositories
{
    public interface IAuthRepository
    {
        Task<AuthClient> GetAuthClient(string clientName);
        Task<User> GetUserById(int userId);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string userName);
        Task<ActionResponseGeneric<int>> AddUser(User user);
        Task<ActionResponse> AddUserLogin(UserLogin userLogin);
        Task<ActionResponse> AddUserRole(UserRole userRole);
        Task<User> GetUserByUserLogin(UserLoginInfo userLoginInfo);
        Task<ActionResponse> ConfirmEmailForUser(int userId);
        Task<bool> EmailExists(string email);
    }

    [PerRequest]
    public class AuthRepository : IAuthRepository
    {
        private readonly IPrivateApiProvider _privateApiProvider;

        public AuthRepository(IPrivateApiProvider privateApiProvider)
        {
            _privateApiProvider = privateApiProvider;
        }

        public async Task<AuthClient> GetAuthClient(string clientName)
        {
            var client = await _privateApiProvider.InvokeGetAsync<AuthClient>("Auth", "GetAuthClient", clientName);
            return client;
        }

        
        public async Task<User> GetUserById(int userId)
        {
            var user = await _privateApiProvider.InvokeGetAsync<User>("UserAccount", "GetUser", userId.ToString());
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            //trailing slash for this get request, or else you get 404 error. An alternative may be to encode the full stops...
            if(!email.EndsWith("/"))
                email = email + "/";

            var user = await _privateApiProvider.InvokeGetAsync<User>("UserAccount", "GetUserByEmail", email);
            return user;
        }

        public async Task<User> GetUserByUsername(string userName)
        {
            var user = await _privateApiProvider.InvokeGetAsync<User>("UserAccount", "GetUserByUsername", userName);
            return user;
        }

        public Task<ActionResponseGeneric<int>> AddUser(User user)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<int>>("UserAccount", "AddUser", user);
        }

        public Task<ActionResponse> AddUserLogin(UserLogin userLogin)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponse>("UserAccount", "AddUserLogin", userLogin);
        }


        public Task<ActionResponse> AddUserRole(UserRole userRole)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponse>("UserAccount", "AddUserRole", userRole);
        }

        
        public async Task<User> GetUserByUserLogin(UserLoginInfo userLoginInfo)
        {
            User user = await _privateApiProvider.InvokePostAsync<User>("UserAccount", "GetUserByUserLogin", userLoginInfo);
            return user;
        }

        public Task<ActionResponse> ConfirmEmailForUser(int userId)
        {
            return _privateApiProvider.InvokeGetAsync<ActionResponse>("UserAccount", "ConfirmEmailForUser", userId);
        }

        public async Task<bool> EmailExists(string email)
        {
            var user = await this.GetUserByEmail(email);
            return user != null;
        }
    }
}