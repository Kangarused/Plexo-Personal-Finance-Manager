using Insight.Database;
using PersonalFinance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.DataAccess
{
    [Sql(Schema = "Security")]
    public interface IUserDataAccess
    {
        // Finds the user by their UserName property
        Task<User> FindUserByUserNameAsync(string userName);

        // Finds the user by their Email
        Task<User> FindUserByEmailAsync(string email);

        // Role stuff
        Task<bool> AddUserToRoleAsync(int userId, string roleName);
        Task<bool> IsUserInRoleAsync(int userId, string roleName);
        Task<IList<string>> GetRolesForUserAsync(int userId);
        Task RemoveUserFromRoleAsync(int userId, string roleName);

        // External Login stuff
        Task<IList<UserLogin>> GetLoginsForUserAsync(int userId);
        Task<User> FindUserByLoginAsync(string loginProvider, string providerKey);

        // User Claims stuff
        Task<IList<UserClaim>> GetUserClaimsAsync(int userId);
        Task RemoveUserClaimAsync(int userId, string claimType);

        // auto procs
        Task<User> SelectUserAsync(int id);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(User user);
        Task InsertUserAsync(User user);
        Task InsertUserLoginAsync(UserLogin userLogin);
        Task DeleteUserLoginAsync(UserLogin login);
        Task InsertUserClaimAsync(UserClaim claim);
    }
}
