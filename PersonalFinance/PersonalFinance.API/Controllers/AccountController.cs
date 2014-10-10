using PersonalFinance.API.Repositories;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PersonalFinance.API.Models;
using System.Threading.Tasks;
using System.Security.Principal;

namespace PersonalFinance.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/accounts")]
    public class AccountController : ApiController
    {
        private AccountManager _accountManager;

        // Manager class for interacting with account data from the database
        public AccountManager AccountManager
        {
            get
            {
                return _accountManager = _accountManager ?? HttpContext.Current.GetOwinContext().Get<AccountManager>();
            }
            private set
            {
                _accountManager = value;
            }
        }

        [HttpGet]
        [Route]
        public async Task<IList<AccountDetailViewModel>> GetAccounts()
        {
            // Retrieve Account data per user id from database
            IList<AccountDetailViewModel> accounts = await AccountManager.GetAccounts();
            if (accounts == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return accounts;
        }

        [HttpGet]
        [Route("{accountId:int}")]
        public async Task<AccountDetailViewModel> GetAccountById(int accountId)
        {
            AccountDetailViewModel account = await AccountManager.GetAccountById(accountId);
            if (account == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return account;
        }

        [HttpPost]
        [Route("new")]
        public async Task<IList<AccountDetailViewModel>> Create(NewAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest); // HTTP STATUS CODE: 400
            }

            bool result = await AccountManager.CreateAccount(model);
            if (!result)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            IList<AccountDetailViewModel> accounts = await AccountManager.GetAccounts();
            if (accounts == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return accounts;
        }

        [HttpDelete]
        [Route("{accountId:int}")]
        public async Task<IList<AccountDetailViewModel>> Delete(int accountId)
        {
            bool result = await AccountManager.DeleteAccount(accountId);
            if (!result)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            IList<AccountDetailViewModel> accounts = await AccountManager.GetAccounts();
            if (accounts == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return accounts;
        }
    }
}
