using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PersonalFinance.Common.Dtos;
using PersonalFinance.Common.Model;
using PersonalFinance.PublicWeb.Providers;

namespace PersonalFinance.PublicWeb.Controllers
{
    public class GroupController : ApiController
    {
        private readonly IPrivateApiProvider _privateApiProvider;
        public GroupController(
            IPrivateApiProvider privateApiProvider
            )
        {
            _privateApiProvider = privateApiProvider;
        }

        [AcceptVerbs("GET")]
        public Task<Group> LoadGroup(int param)
        {
            return _privateApiProvider.InvokeGetAsync<Group>("Group", "LoadGroup", param);
        }

        [AcceptVerbs("GET")]
        public Task<List<Group>> GetUsersGroups()
        {
            return _privateApiProvider.InvokeGetAsync<List<Group>>("Group", "GetUsersGroups");
        }

        [AcceptVerbs("GET")]
        public Task<List<Budget>> GetGroupBudgets(int param)
        {
            return _privateApiProvider.InvokeGetAsync<List<Budget>>("Group", "GetGroupBudgets", param);
        }

        [AcceptVerbs("GET")]
        public Task<List<Bill>> GetGroupBills(int param)
        {
            return _privateApiProvider.InvokeGetAsync<List<Bill>>("Group", "GetGroupBills", param);
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> CreateGroup(CreateGroupRequest request)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<string>>("Group", "CreateGroup", request);
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> LeaveGroup([FromBody] int groupId)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<string>>("Group", "LeaveGroup", groupId);
        }

        [AcceptVerbs("GET")]
        public Task<List<PendingInvites>> LoadPendingInvites(int param)
        {
            return _privateApiProvider.InvokeGetAsync<List<PendingInvites>>("Group", "LoadPendingInvites", param);
        }
    }
}