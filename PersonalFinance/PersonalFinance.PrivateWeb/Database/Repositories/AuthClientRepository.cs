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
    public interface IAuthClientRepository : IAbstractRepository<AuthClient>
    {
        Task<AuthClient> GetAuthClientByName(string name);
    }

    [PerRequest]
    public class AuthClientRepository : AbstractRepository<AuthClient>, IAuthClientRepository
    {
        public AuthClientRepository(IUnitOfWork unitOfWork, IDateResolver dateResolver, IAuditProvider auditProvider) : base(unitOfWork, dateResolver, auditProvider)
        {
        }

        public async Task<AuthClient> GetAuthClientByName(string name)
        {
            var query = Db.From<AuthClient>()
                .Where(i => i.Name == name);

            var results = await Db.SelectAsync(query);
            return results.SingleOrDefault();
        }
    }
}