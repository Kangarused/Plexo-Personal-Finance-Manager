using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PersonalFinance.API.DomainModels
{
    public class User : IUser<int>
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Guid string of the Household the user belongs to
        /// </summary>
        public Guid Household { get; set; }

        /// <summary>
        /// User's Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The User's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The hashed form of the user's password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// A random value that should change when a users 
        /// credentials have changed (password change, login removed)
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// Soft delete
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Check if User is locked out of account
        /// </summary>
        public bool IsLockedOut { get; set; }

        /// <summary>
        /// Token issued to reset password
        /// </summary>
        public string PasswordResetToken { get; set; }

        /// <summary>
        /// Date of Password Reset Token expiration
        /// </summary>
        public DateTimeOffset? PasswordResetExpiry { get; set; }

        /// <summary>
        /// DateTime in UTC when lockout ends, anytime in the past
        /// is considered not locked out.
        /// </summary>
        public DateTimeOffset LockoutEndDate { get; set; }

        /// <summary>
        /// True if user confirmed account by email.
        /// False by default.
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Check if TwoFactor authentication is enabled
        /// for this user
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Check if lockout is enabled for this user
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Field to record user login failures for
        /// the purposes of lockout
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// User's Phone Number 
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Check if Phone Number is confirmed by User
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// User Constructor
        /// </summary>
        public User()
        {
            // Automatically assign the user their own household Id
            Household = Guid.NewGuid();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}