using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using PersonalFinance.PrivateWeb.Database.OrmLiteInfrastructure;
using ServiceStack.OrmLite;

namespace PersonalFinance.PrivateWeb.Database.Repositories
{

    public interface IUserLoginRepository : IAbstractRepository<UserLogin>
    {
        Task<UserLogin> GetByLoginProviderAndKeyAsync(string loginprovider, string providerKey);
    }
    [PerRequest]
    public class UserLoginRepository : AbstractRepository<UserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(IUnitOfWork unitOfWork, IDateResolver dateResolver, IAuditProvider auditProvider) : base(unitOfWork, dateResolver, auditProvider)
        {
        }

        public async Task<UserLogin> GetByLoginProviderAndKeyAsync(string loginprovider, string providerKey)
        {
            var query = Db.From<UserLogin>()
                .Where(i => i.ProviderKey == providerKey && i.LoginProvider == loginprovider);

            var results = await Db.SelectAsync(query);
            return results.SingleOrDefault();
        }
    }
}