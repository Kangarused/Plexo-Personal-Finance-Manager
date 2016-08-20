//using PersonalFinance.Web.Models;
//using PersonalFinance.Web.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;
//using Microsoft.AspNet.Identity.Owin;
//using PersonalFinance.Web.DomainModels;
//using System.Linq.Dynamic;

//namespace PersonalFinance.PublicWeb.Controllers
//{
//    [Authorize]
//    [RoutePrefix("api/transactions")]
//    public class TransactionsController : ApiController
//    {
//        private AccountManager _accountManager;

//        // Manager class for interacting with account data from the database
//        public AccountManager AccountManager
//        {
//            get
//            {
//                return _accountManager = _accountManager ?? HttpContext.Current.GetOwinContext().Get<AccountManager>();
//            }
//            private set
//            {
//                _accountManager = value;
//            }
//        }

//        [HttpGet]
//        [Route]
//        public async Task<IHttpActionResult> GetTransactions(
//            int accountId,
//            int page = 1,
//            int itemsPerPage = 10, 
//            string sortBy = "TransactionDate", 
//            bool reverse = false,
//            string description = null,
//            decimal? minAmount = null, 
//            decimal? maxAmount = null,
//            bool? isReconciled = null,
//            string category = null,
//            DateTimeOffset? minDate = null,
//            DateTimeOffset? maxDate = null)
//        {
//            var transactions = (await AccountManager.GetTransactions(accountId)).AsQueryable();
//            if (transactions == null)
//                throw new HttpResponseException(HttpStatusCode.NotFound);

//            // searching
//            if (!string.IsNullOrEmpty(description))
//                transactions = transactions.Where(t => t.Description.ToLower() == description.ToLower());

//            if (minAmount != null)
//                transactions = transactions.Where(t => minAmount <= t.Amount);

//            if (maxAmount != null)
//                transactions = transactions.Where(t => t.Amount <= maxAmount);

//            if (isReconciled != null)
//                transactions = transactions.Where(t => t.isReconciled == isReconciled);

//            if (category != null)
//                transactions = transactions.Where(t => t.Category.ToLower() == category.ToLower());

//            if (minDate != null)
//                transactions = transactions.Where(t => minDate <= t.TransactionDate);

//            if (maxDate != null)
//                transactions = transactions.Where(t => t.TransactionDate <= maxDate);
         
//            // sorting
//            transactions = transactions.OrderBy(sortBy + (reverse ? " descending" : ""));

//            // paging
//            var pagedTransactions = transactions.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

//            var json = new
//            {
//                count = transactions.Count(),
//                data = pagedTransactions.Select(t => new TransactionViewModel
//                {
//                    Id = t.Id,
//                    AccountId = t.Id,
//                    Description = t.Description,
//                    Category = t.Category,
//                    Amount = t.Amount,
//                    Reconciled = t.Reconciled,
//                    isReconciled = t.isReconciled,
//                    TransactionDate = t.TransactionDate,
//                    CreatedOn = t.CreatedOn,
//                    UpdatedBy = t.UpdatedBy,
//                    UpdatedOn = t.UpdatedOn
//                })
//            };

//            return Ok(json);
//        }

//        [HttpGet]
//        [Route("search")]
//        public async Task<IList<TransactionViewModel>> SearchForTransactions(SearchTransactionViewModel model)
//        {
//            IList<TransactionViewModel> transactions = await AccountManager.SearchForTransactions(model);

//            return transactions;
//        }

//        [HttpPost]
//        [Route("new")]
//        public async Task<IList<TransactionViewModel>> CreateTransaction(NewTransactionViewModel model)
//        {
//            bool result = await AccountManager.CreateTransaction(model);
//            if (!result)
//                throw new HttpResponseException(HttpStatusCode.InternalServerError);

//            IList<TransactionViewModel> transactions = await AccountManager.GetTransactions(model.AccountId);

//            return transactions;
//        }

//        [HttpPut]
//        [Route("update")]
//        public async Task<IList<TransactionViewModel>> UpdateTransaction(TransactionViewModel model)
//        {
//            // update transaction
//            bool result = await AccountManager.UpdateTransaction(model);
//            if (!result)
//                throw new HttpResponseException(HttpStatusCode.InternalServerError);

//            // return updated transactions
//            IList<TransactionViewModel> transactions = await AccountManager.GetTransactions(model.AccountId);

//            return transactions;
//        }

//        [HttpDelete]
//        [Route("delete")]
//        public async Task<IList<TransactionViewModel>> DeleteTransaction(int transactionId, string categoryName, int accountId)
//        {
//            // Get the category id
//            Category category = await AccountManager.GetCategoryByName(categoryName);
//            int categoryId = category.Id;

//            // Delete entry from dbo.TransactionCategories join table corresponding to given transaction id and category id
//            bool result1 = await AccountManager.DeleteTransactionCategory(transactionId, categoryId);
//            if (!result1)
//                throw new HttpResponseException(HttpStatusCode.InternalServerError);

//            // Delete the transaction
//            bool result2 = await AccountManager.DeleteTransaction(transactionId);
//            if (!result2)
//                throw new HttpResponseException(HttpStatusCode.InternalServerError);

//            // return updated list of transactions
//            IList<TransactionViewModel> transactions = await AccountManager.GetTransactions(accountId);

//            return transactions;
//        }
//    }
//}
