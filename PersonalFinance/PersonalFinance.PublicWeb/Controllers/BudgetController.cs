using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PersonalFinance.Common.Dtos;
using PersonalFinance.Common.Model;
using PersonalFinance.PublicWeb.Providers;

namespace PersonalFinance.PublicWeb.Controllers
{
    public class BudgetController : ApiController
    {
        private readonly IPrivateApiProvider _privateApiProvider;

        public BudgetController(IPrivateApiProvider privateApiProvider)
        {
            _privateApiProvider = privateApiProvider;
        }

        [AcceptVerbs("GET")]
        public Task<Budget> GetBudgetById(int param)
        {
            return _privateApiProvider.InvokeGetAsync<Budget>("Budget", "GetBudgetById", param);
        }

        [AcceptVerbs("GET")]
        public Task<BudgetDetailsResponse> GetBudgetDetailsById(int param)
        {
            return _privateApiProvider.InvokeGetAsync<BudgetDetailsResponse>("Budget", "GetBudgetDetailsById", param);
        }

        [AcceptVerbs("GET")]
        public Task<List<Budget>> GetBudgetsForUser()
        {
            return _privateApiProvider.InvokeGetAsync<List<Budget>>("Budget", "GetBudgetsForUser");
        }

        [AcceptVerbs("GET")]
        public Task<List<BudgetItem>> GetBudgetItemsForBudget(int param)
        {
            return _privateApiProvider.InvokeGetAsync<List<BudgetItem>>("Budget", "GetBudgetItemsForBudget", param);
        }

        [AcceptVerbs("GET")]
        public Task<RecentBudgetResponse> GetRecentBudgetItems()
        {
            return _privateApiProvider.InvokeGetAsync<RecentBudgetResponse>("Budget", "GetRecentBudgetItems");
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> AddBudget(Budget request)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<string>>("Budget", "AddBudget", request);
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> DeleteBudgetById(Budget request)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<string>>("Budget", "DeleteBudgetById", request);
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> AddBudgetItem(BudgetItem request)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<string>>("Budget", "AddBudgetItem", request);
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> DeleteBudgetItem(BudgetItem request)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<string>>("Budget", "DeleteBudgetItem", request);
        }
    }
}