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
    public interface IGroupMemberRepository : IAbstractRepository<GroupMember>
    {
        Task<List<GroupMember>> GetAllMembersForGroup(int groupId);
        Task<GroupMember> GetByUserIdForGroup(int userId, int groupId);
        Task<List<GroupMember>> GetGroupListForUser(int userId);
    }

    [PerRequest]
    public class GroupMemberRepository : AbstractRepository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRepository(IUnitOfWork unitOfWork,
            IDateResolver dateResolver, IAuditProvider auditProvider) 
            : base(unitOfWork, dateResolver, auditProvider)
        {
        }

        public async Task<List<GroupMember>> GetAllMembersForGroup(int groupId)
        {
            var query = Db.From<GroupMember>().Where(x => x.GroupId == groupId);
            var result = await Db.LoadSelectAsync(query);

            return result;
        }

        public async Task<GroupMember> GetByUserIdForGroup(int userId, int groupId)
        {
            var query = Db.From<GroupMember>().Where(x => x.UserId == userId && x.GroupId == groupId);
            var result = await Db.SelectAsync(query);

            return result.FirstOrDefault();
        }

        public async Task<List<GroupMember>> GetGroupListForUser(int userId)
        {
            var query = Db.From<GroupMember>().Where(x => x.UserId == userId);
            var result = await Db.LoadSelectAsync(query);

            return result;
        }
    }
}