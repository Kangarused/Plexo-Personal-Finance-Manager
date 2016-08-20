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
    public interface IBudgetItemRepository : IAbstractRepository<BudgetItem>
    {
        Task<List<BudgetItem>> GetBudgetItemsForBudget(int budgetId);
    }

    [PerRequest]
    public class BudgetItemRepository : AbstractRepository<BudgetItem>, IBudgetItemRepository
    {
        public BudgetItemRepository(IUnitOfWork unitOfWork,
            IDateResolver dateResolver,
            IAuditProvider auditProvider) : base(unitOfWork, dateResolver, auditProvider) { }

        public async Task<List<BudgetItem>> GetBudgetItemsForBudget(int budgetId)
        {
            var query = Db.From<BudgetItem>().Where(x => x.BudgetId == budgetId);
            var results = await Db.LoadSelectAsync(query);

            return results;
        }
    }
}