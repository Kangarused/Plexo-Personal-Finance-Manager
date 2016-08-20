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
    public interface IAccountRepository : IAbstractRepository<Account>
    {
        Task<List<Account>> GetAccountsForUser(int userId);
        Task<int> DeleteAccountById(int accountId);
    }

    [PerRequest]
    public class AccountRepository : AbstractRepository<Account>, IAccountRepository
    {
        public AccountRepository(IUnitOfWork unitOfWork, IDateResolver dateResolver, IAuditProvider auditProvider) : base(unitOfWork, dateResolver, auditProvider)
        {
        }

        public async Task<List<Account>> GetAccountsForUser(int userId)
        {
            var query = Db.From<Account>().Where(x => x.UserId == userId);
            var results = await Db.LoadSelectAsync(query);

            return results;
        }

        public async Task<int> DeleteAccountById(int accountId)
        {
            var result = await Db.DeleteByIdAsync<Account>(accountId);
            return result;
        }
    }
}