using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using PersonalFinance.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Insight.Database;
using System.Threading.Tasks;
using PersonalFinance.Models;
using PersonalFinance.DomainModels;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace PersonalFinance.Repositories
{
    public class AccountManager : IDisposable
    {
        private readonly IAccountDataAccess _accountData;
        private readonly ApplicationUserManager _userManager;

        public AccountManager(IAccountDataAccess accountData)
        {
            _accountData = accountData;
            _userManager = _userManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();
        }

        // callback function for IAppBuilder.CreatePerOwinContext
        public static AccountManager Create(IdentityFactoryOptions<AccountManager> options, IOwinContext context)
        {
            IAccountDataAccess accountData = context.Get<SqlConnection>().As<IAccountDataAccess>();
            AccountManager manager = new AccountManager(accountData);

            return manager;
        }

        public async Task<IList<AccountDetailViewModel>> GetAccounts()
        {
            var username = HttpContext.Current.User.Identity.Name;
            User user = await _userManager.FindByNameAsync(username);

            IList<AccountDetailViewModel> accounts = await _accountData.GetAccountsByHouseholdAsync(user.Household);

            return accounts;
        }

        public async Task<AccountDetailViewModel> GetAccountById(int accountId)
        {
            AccountDetailViewModel account = await _accountData.GetAccountByIdAsync(accountId);

            return account;
        }

        public async Task<bool> CreateAccount(NewAccountViewModel model)
        {
            var username = HttpContext.Current.User.Identity.Name;
            User user = await _userManager.FindByNameAsync(username);

            Account account = new Account
            {
                UserId = user.Id,
                Household = user.Household,
                Name = model.Name
            };

            // Id of newly inserted account is returned.
            bool result = await _accountData.InsertAccountAsync(account);

            return result;
        }

        public async Task<bool> DeleteAccount(int accountId)
        {
            bool result = await _accountData.DeleteAccountAsync(accountId);

            return result;
        }

        public async Task<IList<TransactionViewModel>> GetTransactions(int accountId)
        {
            IList<TransactionViewModel> transactions = await _accountData.GetTransactionsByAccountIdAsync(accountId);

            return transactions;
        }

        public async Task<IList<TransactionViewModel>> SearchForTransactions(SearchTransactionViewModel model)
        {
            IList<TransactionViewModel> transactions = await _accountData.SearchForTransactionsAsync(model);

            return transactions;
        }

        public async Task<bool> CreateTransaction(NewTransactionViewModel model)
        {
            // Get the authenticated user
            var username = HttpContext.Current.User.Identity.Name;
            User user = await _userManager.FindByNameAsync(username);

            // First retrieve all categories.
            // Select the category specified by submitted transaction data.
            // If no such category exists, create it and get the category id.
            IList<Category> categories = await _accountData.GetCategoriesAsync();
            Category category = categories.FirstOrDefault(c => c.Name == model.Category);
            int categoryId;
            if (category == null)
                categoryId = await _accountData.InsertCategoryAsync(new Category
                {
                    Name = model.Category
                });
            else
                categoryId = category.Id;


            // Insert New Transaction
            Transaction transaction = new Transaction
            {
                AccountId = model.AccountId,
                Description = model.Description,
                Amount = model.Amount,
                Reconciled = model.Reconciled,
                isReconciled = model.Amount.Equals(model.Reconciled),
                TransactionDate = model.TransactionDate,
                Updated_By = user.Id
            };
            int transactionId = await _accountData.InsertTransactionAsync(transaction);

            // Insert entry in the TransactionCategories join table
            // to associate the newly created transaction with it's category
            bool result = await _accountData.InsertTransactionCategoryAsync(transactionId, categoryId);

            return result;
        }

        public async Task<bool> UpdateTransaction(TransactionViewModel model)
        {
            // Get the authenticated user
            var username = HttpContext.Current.User.Identity.Name;
            User user = await _userManager.FindByNameAsync(username);

            // First retrieve all categories.
            // Select the category specified by submitted transaction data.
            // If no such category exists, create it and get the category id.
            IList<Category> categories = await _accountData.GetCategoriesAsync();
            Category category = categories.FirstOrDefault(c => c.Name == model.Category);
            int categoryId;
            if (category == null)
                categoryId = await _accountData.InsertCategoryAsync(new Category
                {
                    Name = model.Category
                });
            else
                categoryId = category.Id;

            Transaction transaction = new Transaction
            {
                Id = model.Id,
                Description = model.Description,
                Amount = model.Amount,
                Reconciled = model.Reconciled,
                isReconciled = model.Amount.Equals(model.Reconciled),
                Updated_By = user.Id
            };
            bool updateTransactionResult = await _accountData.UpdateTransactionAsync(transaction);

            // Update the category associated with this transaction
            bool updatedTransactionCategoryResult = await _accountData.UpdateTransactionCategoryAsync(transaction.Id, categoryId);

            return updatedTransactionCategoryResult;
        }

        public async Task<bool> DeleteTransactionCategory(int transactionId, int categoryId)
        {
            bool result = await _accountData.DeleteTransactionCategoryAsync(transactionId, categoryId);

            return result;
        }

        public async Task<bool> DeleteTransaction(int transactionId)
        {
            bool result = await _accountData.DeleteTransactionAsync(transactionId);

            return result;
        }

        public async Task<IList<Category>> GetCategories()
        {
            IList<Category> categories = await _accountData.GetCategoriesAsync();

            return categories;
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            Category category = await _accountData.GetCategoryByNameAsync(categoryName);

            return category;
        }

        public async Task<int> CreateCategory(Category category)
        {
            int Id = await _accountData.InsertCategoryAsync(category);

            return Id;
        }

        public void Dispose()
        {
            // Dispose of resources here, if needed
        }

        public async Task<IList<BudgetItemViewModel>> GetBudgetItems()
        {
            var username = HttpContext.Current.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            IList<BudgetItemViewModel> budgetItems = await _accountData.GetBudgetItemsByHouseholdAsync(user.Household);

            return budgetItems;
        }
    }
}