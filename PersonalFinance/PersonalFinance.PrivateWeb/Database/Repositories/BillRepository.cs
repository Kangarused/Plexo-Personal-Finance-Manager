using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using PersonalFinance.PrivateWeb.Database.OrmLiteInfrastructure;
using ServiceStack.OrmLite;

namespace PersonalFinance.PrivateWeb.Database.Repositories
{
    public interface IBillRepository : IAbstractRepository<Bill>
    {
        Task<List<Bill>> GetBillsForUser(int userId);
        Task<List<Bill>> GetBillsForGroup(int groupId);
    }

    [PerRequest]
    public class BillRepository : AbstractRepository<Bill>, IBillRepository
    {
        public BillRepository(IUnitOfWork unitOfWork,
            IDateResolver dateResolver,
            IAuditProvider auditProvider) : base(unitOfWork, dateResolver, auditProvider)
        {
        }

        public async Task<List<Bill>> GetBillsForUser(int userId)
        {
            var query = Db.From<Bill>().Where(x => x.UserId == userId);
            var response = await Db.LoadSelectAsync(query);

            return response;
        }

        public async Task<List<Bill>> GetBillsForGroup(int groupId)
        {
            var query = Db.From<Bill>().Where(x => x.GroupId == groupId);
            var response = await Db.LoadSelectAsync(query);

            return response;
        }
    }
}