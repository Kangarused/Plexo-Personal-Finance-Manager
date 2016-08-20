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
        public Task<List<Account>> GetAccounts()
        {
            return _privateApiProvider.InvokeGetAsync<List<Account>>("Account", "GetAccounts");
        }

        [AcceptVerbs("GET")]
        public Task<Account> GetAccountById(int param)
        {
            return _privateApiProvider.InvokeGetAsync<Account>("Account", "GetAccountById", param);
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> CreateAccount(Account param)
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
