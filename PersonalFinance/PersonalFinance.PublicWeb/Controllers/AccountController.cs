using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using PersonalFinance.Common.Model;
using PersonalFinance.PublicWeb.Providers;

namespace PersonalFinance.PublicWeb.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IPrivateApiProvider _privateApiProvider;

        public AccountController(IPrivateApiProvider privateApiProvider)
        {
            _privateApiProvider = privateApiProvider;
        }

        [AcceptVerbs("GET")]
        public Task<List<Group>> GetAccounts()
        {
            return _privateApiProvider.InvokeGetAsync<List<Group>>("Account", "GetAccounts");
        }

        [AcceptVerbs("GET")]
        public Task<Group> GetAccountById(int param)
        {
            return _privateApiProvider.InvokeGetAsync<Group>("Account", "GetAccountById", param);
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> CreateAccount(Group param)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<string>>("Account", "CreateAccount", param);
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> DeleteAccount(int param)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<string>>("Account", "DeleteAccount", param);
        }
    }
}
