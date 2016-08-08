using Insight.Database;
using PersonalFinance.DomainModels;
using PersonalFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.DataAccess
{
    [Sql(Schema = "dbo")]
    public interface IAccountDataAccess
    {
        Task<bool> InsertAccountAsync(Account account);
        Task<bool> DeleteAccountAsync(int accountId);

        Task<AccountDetailViewModel> GetAccountByIdAsync(int accountId);
        Task<IList<AccountDetailViewModel>> GetAccountsByHouseholdAsync(Guid household);

        // Transactions
        Task<IList<TransactionViewModel>> GetTransactionsByAccountIdAsync(int accountId);

        Task<IList<TransactionViewModel>> SearchForTransactionsAsync(SearchTransactionViewModel model); // NEED TO IMPLEMENT STORED PROCEDURE!!!!!

        Task<int> InsertTransactionAsync(Transaction newTransaction);
        Task<bool> UpdateTransactionAsync(Transaction editTransaction);
        Task<bool> DeleteTransactionAsync(int transactionId);

        // TransactionCategories
        Task<bool> InsertTransactionCategoryAsync(int transactionId, int categoryId);
        Task<bool> UpdateTransactionCategoryAsync(int transactionId, int categoryId);
        Task<bool> DeleteTransactionCategoryAsync(int transactionId, int categoryId);

        // Categories
        Task<int> InsertCategoryAsync(Category category);
        Task<IList<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByNameAsync(string categoryName);

        // Budget Items
        Task<IList<BudgetItemViewModel>> GetBudgetItemsByHouseholdAsync(Guid Household);
    }
}
