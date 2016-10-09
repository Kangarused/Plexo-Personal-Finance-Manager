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
    public interface IGroupInviteRepository : IAbstractRepository<GroupInvite>
    {
        Task<List<GroupInvite>> GetRecentInvitesForGroup(int groupId);
    }

    [PerRequest]
    public class GroupInviteRepository : AbstractRepository<GroupInvite>, IGroupInviteRepository
    {
        public GroupInviteRepository(IUnitOfWork unitOfWork, 
            IDateResolver dateResolver, IAuditProvider auditProvider) 
            : base(unitOfWork, dateResolver, auditProvider)
        {
        }

        public async Task<List<GroupInvite>> GetRecentInvitesForGroup(int groupId)
        {
            var query = Db.From<GroupInvite>().Where(x => x.GroupId == groupId).OrderByDescending(x => x.DateSent).Limit(10);
            var results = await Db.LoadSelectAsync(query);
            return results;
        }
    }
}