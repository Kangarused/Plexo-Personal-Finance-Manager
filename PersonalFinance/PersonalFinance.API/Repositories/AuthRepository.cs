using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PersonalFinance.API.Models;
using PersonalFinance.API.DomainModels;

namespace PersonalFinance.API
{
    public class AuthRepository : IDisposable
    {
        private ApplicationUserManager _userManager;

        public AuthRepository() { }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager = _userManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // TODO: Error handling and check that password and the confirmation password 
        // entries are the same.
        public async Task<IdentityResult> RegisterUser(RegisterUserViewModel model)
        {
            User user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.FirstName + " " + model.LastName
            };

            var result = await UserManager.CreateAsync(user, model.Password);

            return result;
        }
        // TODO: Error handling
        public async Task<User> FindUser(string userName, string password)
        {
            User user = await UserManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            UserManager.Dispose();
        }
    }
}