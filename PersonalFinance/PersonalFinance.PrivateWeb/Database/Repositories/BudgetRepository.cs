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
    public interface IBudgetRepository : IAbstractRepository<Budget>
    {
        Task<List<Budget>> GetBudgetsForUser(int userId);
        Task<Budget> GetMostRecentBudgetForUser(int userId);
    }

    [PerRequest]
    public class BudgetRepository : AbstractRepository<Budget>, IBudgetRepository
    {
        public BudgetRepository(IUnitOfWork unitOfWork, 
            IDateResolver dateResolver, 
            IAuditProvider auditProvider) : base(unitOfWork, dateResolver, auditProvider) {}

        public async Task<List<Budget>> GetBudgetsForUser(int userId)
        {
            var query = Db.From<Budget>().Where(x => x.UserId == userId);
            var results = await Db.LoadSelectAsync(query);

            return results;
        }

        public async Task<Budget> GetMostRecentBudgetForUser(int userId)
        {
            var query = Db.From<Budget>().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedTime);
            var results = await Db.LoadSelectAsync(query);

            return results.FirstOrDefault();
        }
    }
}