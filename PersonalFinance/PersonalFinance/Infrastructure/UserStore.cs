using Microsoft.AspNet.Identity;
using PersonalFinance.DataAccess;
using PersonalFinance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PersonalFinance.Infrastructure
{
    public class UserStore :
        IUserStore<User, int>,
        IUserLoginStore<User, int>,
        IUserPasswordStore<User, int>,
        IUserSecurityStampStore<User, int>,
        IUserEmailStore<User, int>,
        IUserPhoneNumberStore<User, int>,
        IUserClaimStore<User, int>,
        IUserRoleStore<User, int>,
        IUserTwoFactorStore<User, int>,
        IUserLockoutStore<User, int>
    {
        private readonly IUserDataAccess _userData;

        public UserStore(IUserDataAccess userData)
        {
            _userData = userData;
        }

        public void Dispose()
        {
        }

        #region IUserStore
        public Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return _userData.InsertUserAsync(user);
        }

        public Task DeleteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return _userData.DeleteUserAsync(user.Id);
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            var user = await _userData.SelectUserAsync(userId);
            return user;
        }

        public async Task<User> FindByNameAsync(string UserName)
        {
            if (string.IsNullOrWhiteSpace(UserName))
                throw new ArgumentNullException("UserName");

            var user = await _userData.FindUserByUserNameAsync(UserName);
            return user;
        }

        public Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return _userData.UpdateUserAsync(user);
        }
        #endregion

        #region IUserLoginStore
        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (login == null)
                throw new ArgumentNullException("login");

            UserLogin userLogin = new UserLogin
            {
                UserId = user.Id,
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey
            };

            return _userData.InsertUserLoginAsync(userLogin);
        }

        public async Task<User> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            var user = await _userData.FindUserByLoginAsync(login.LoginProvider, login.ProviderKey);
            return user;
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var logins = await _userData.GetLoginsForUserAsync(user.Id);

            return logins
                .Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey))
                .ToList();
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (login == null)
                throw new ArgumentNullException("login");

            var userLogin = new UserLogin
            {
                UserId = user.Id,
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey
            };

            return _userData.DeleteUserLoginAsync(userLogin);
        }
        #endregion

        #region IUserPasswordStore
        public virtual Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public virtual Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (passwordHash == null)
                throw new ArgumentNullException("passwordHash");

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }
        #endregion

        #region IUserSecurityStore
        public virtual Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(stamp))
                throw new ArgumentNullException("stamp");

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }
        #endregion

        #region IUserEmailStore
        public async Task<User> FindByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            var user = await _userData.FindUserByEmailAsync(email);
            return user;
        }

        public Task<string> GetEmailAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailAsync(User user, string email)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            return Task.FromResult(user.Email = email);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.EmailConfirmed = confirmed);
        }
        #endregion

        #region IUserPhoneNumberStore
        public Task<string> GetPhoneNumberAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException("phoneNumber");

            user.PhoneNumber = phoneNumber;

            return Task.FromResult(0);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PhoneNumberConfirmed = confirmed;

            return Task.FromResult(0);
        }
        #endregion

        #region IUserClaimStore
        public Task AddClaimAsync(User user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (claim == null)
                throw new ArgumentNullException("claim");

            var userClaim = new UserClaim
            {
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };

            return _userData.InsertUserClaimAsync(userClaim);
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var userClaims = await _userData.GetUserClaimsAsync(user.Id);
            var claims = userClaims
                            .Select(x => new Claim(x.ClaimType, x.ClaimValue))
                            .ToList();

            if (user.Name != null)
                claims.Add(new Claim(ClaimTypes.GivenName, user.Name));

            return claims;

        }

        public Task RemoveClaimAsync(User user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (claim == null)
                throw new ArgumentNullException("claim");

            return _userData.RemoveUserClaimAsync(user.Id, claim.Type);
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(User user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            return _userData.AddUserToRoleAsync(user.Id, roleName);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return _userData.GetRolesForUserAsync(user.Id);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            return _userData.IsUserInRoleAsync(user.Id, roleName);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            return _userData.RemoveUserFromRoleAsync(user.Id, roleName);
        }
        #endregion

        #region IUserTwoFactorStore
        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (enabled == null)
                throw new ArgumentNullException("enabled");

            user.TwoFactorEnabled = enabled;

            return Task.FromResult(0);
        }
        #endregion

        #region IUserLockoutStore
        public Task<int> GetAccessFailedCountAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.LockoutEndDate);
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.AccessFailedCount += 1;

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.AccessFailedCount = 0);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (enabled == null)
                throw new ArgumentNullException("enabled");

            return Task.FromResult(user.LockoutEnabled = enabled);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (lockoutEnd == null)
                throw new ArgumentNullException("lockoutEnd");

            return Task.FromResult(user.LockoutEndDate = lockoutEnd);
        }
        #endregion
    }
}