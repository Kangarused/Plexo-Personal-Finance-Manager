using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using PersonalFinance.PrivateWeb.Database.OrmLiteInfrastructure;

namespace PersonalFinance.PrivateWeb.Database.Repositories
{
    public interface IGroupRepository : IAbstractRepository<Group>
    {
        
    }

    [PerRequest]
    public class GroupRepository : AbstractRepository<Group>, IGroupRepository
    {
        public GroupRepository(IUnitOfWork unitOfWork, 
            IDateResolver dateResolver, IAuditProvider auditProvider) 
            : base(unitOfWork, dateResolver, auditProvider)
        {
        }


    }
}