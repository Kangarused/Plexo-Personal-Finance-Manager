using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PersonalFinance.PrivateWeb.Database.OrmLiteInfrastructure;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using ServiceStack.OrmLite;

namespace PersonalFinance.PrivateWeb.Database.Repositories
{
    public interface IUserRepository : IAbstractRepository<User>
    {
        Task<User> LoadByEmailAsync(string email);
        Task<User> LoadByUsernameAsync(string userName);
    }

    [PerRequest]
    public class UserRepository : AbstractRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork, IDateResolver dateResolver, IAuditProvider auditProvider) : base(unitOfWork, dateResolver, auditProvider)
        {
        }

        public async Task<User> LoadByEmailAsync(string email)
        {
            var query = Db.From<User>()
                .Where(i => i.Email == email);

            var results = await Db.LoadSelectAsync(query);
            return results.SingleOrDefault();
        }

        public async Task<User> LoadByUsernameAsync(string userName)
        {
            var query = Db.From<User>()
                .Where(i => i.UserName == userName);

            var results = await Db.LoadSelectAsync(query);
            return results.SingleOrDefault();
        }
    }
}