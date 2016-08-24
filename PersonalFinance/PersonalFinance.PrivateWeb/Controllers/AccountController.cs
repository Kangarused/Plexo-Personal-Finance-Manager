using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using PersonalFinance.Common.Model;
using PersonalFinance.PrivateWeb.Database.Repositories;
using PersonalFinance.PrivateWeb.Providers;

namespace PersonalFinance.PrivateWeb.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IUserResolver _userResolver;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountController(
            IUserResolver userResolver,
            IUserRepository userRepository,
            IAccountRepository accountRepository
            )
        {
            _userResolver = userResolver;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        [AcceptVerbs("GET")]
        public async Task<List<Account>> GetAccounts()
        {
            var currentUser = _userResolver.GetUser();
            var accounts = await _accountRepository.GetAccountsForUser(currentUser.Id);

            return accounts;
        }

        [AcceptVerbs("GET")]
        public async Task<Account> GetAccountById(int param)
        {
            var currentUser = _userResolver.GetUser();
            var account = await _accountRepository.GetByIdAsync(param);

            if (account.UserId == currentUser.Id)
            {
                return account;
            }

            throw new AccessViolationException("User does not have access to that account");
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<string>> CreateAccount(Account newAccount)
        {
            var currentUser = _userResolver.GetUser();
            newAccount.UserId = currentUser.Id;
            newAccount.CreatedBy = currentUser.DisplayName;
            newAccount.CreatedTime = DateTime.Now;
            newAccount.ModifiedBy = currentUser.DisplayName;
            newAccount.ModifiedTime = DateTime.Now;

            //todo: add validation

            await _accountRepository.InsertAsync(newAccount);

            return new ActionResponseGeneric<string>()
            {
                Succeed = true,
                Response = "Account created successfully"
            };
      

            //return new ActionResponseGeneric<string>()
            //{
            //    Succeed = false,
            //    Response = "Account creation failed"
            //};
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<string>> DeleteAccount(int param)
        {
            var account = await _accountRepository.GetByIdAsync(param);
            var currentUser = _userResolver.GetUser();

            if (account.UserId == currentUser.Id)
            {
                await _accountRepository.DeleteAccountById(param);
                return new ActionResponseGeneric<string>()
                {
                    Succeed = true,
                    Response = "Account deleted successfully"
                };
            }

            throw new AccessViolationException("User does not have access to that account");
        }
    }
}
