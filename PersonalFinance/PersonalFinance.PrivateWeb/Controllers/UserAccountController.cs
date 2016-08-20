using System;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using PersonalFinance.Common.Model;
using PersonalFinance.PrivateWeb.Database.Repositories;
using PersonalFinance.PrivateWeb.WebApiFilters;

namespace PersonalFinance.PrivateWeb.Controllers
{
    public class UserAccountController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserLoginRepository _userLoginRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserAccountController(
            IUserRepository userRepository,
            IUserLoginRepository userLoginRepository,
            IUserRoleRepository userRoleRepository
            )
        {
            _userRepository = userRepository;
            _userLoginRepository = userLoginRepository;
            _userRoleRepository = userRoleRepository;
        }

        [WebApiFilters.Authorize(Role.AnonymousOverPrivateApi)]
        [AcceptVerbs("GET")]
        public Task<User> GetUser(int param)
        {
            return _userRepository.LoadByIdAsync(param);
        }

        [WebApiFilters.Authorize(Role.AnonymousOverPrivateApi)]
        [AcceptVerbs("GET")]
        public Task<User> GetUserByEmail(string param)
        {
            return _userRepository.LoadByEmailAsync(param);
        }

        [WebApiFilters.Authorize(Role.AnonymousOverPrivateApi)]
        [AcceptVerbs("GET")]
        public Task<User> GetUserByUsername(string param)
        {
            return _userRepository.LoadByUsernameAsync(param);
        }

        [WebApiFilters.Authorize(Role.AnonymousOverPrivateApi)]
        [AcceptVerbs("GET")]
        public async Task<ActionResponse> ConfirmEmailForUser(int param)
        {
            var user = await _userRepository.LoadByIdAsync(param);
            if (user == null)
            {
                return ActionResponse.CreateWithErrors(new[] { "User not found" });
            }

            user.EmailConfirmed = true;

            await _userRepository.UpdateAsync(user);
            return new ActionResponse { Succeed = true };
        }

        [WebApiFilters.Authorize(Role.AnonymousOverPrivateApi)]
        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<long>> AddUser(User user)
        {
            //Fill Audit Data
            user.EmailConfirmed = false;
            user.CreatedBy = "server";
            user.CreatedTime = DateTime.Now;
            user.ModifiedBy = "server";
            user.ModifiedTime = DateTime.Now;

            var userId = await _userRepository.InsertAsync(user);
            return new ActionResponseGeneric<long>()
            {
                Succeed = true,
                Response = userId
            };
        }

        [WebApiFilters.Authorize(Role.AnonymousOverPrivateApi)]
        [AcceptVerbs("POST")]
        public async Task<User> GetUserByUserLogin(UserLoginInfo loginInfo)
        {
            User user = null;
            var userLogin = await _userLoginRepository.GetByLoginProviderAndKeyAsync(loginInfo.LoginProvider,
                loginInfo.ProviderKey);
            if (userLogin != null)
            {
                user = await _userRepository.LoadByIdAsync(userLogin.UserId);
            }
            return user;
        }

        [WebApiFilters.Authorize(Role.AnonymousOverPrivateApi)]
        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<bool>> AddUserLogin(UserLogin userLogin)
        {
            var userLoginId = await _userLoginRepository.InsertAsync(userLogin);
            return new ActionResponseGeneric<bool>()
            {
                Succeed = true,
                Response = true
            };
        }

        [WebApiFilters.Authorize(Role.AnonymousOverPrivateApi)]
        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<bool>> AddUserRole(UserRole userRole)
        {
            var userLoginId = await _userRoleRepository.InsertAsync(userRole);
            return new ActionResponseGeneric<bool>()
            {
                Succeed = true,
                Response = true
            };
        }
    }
}