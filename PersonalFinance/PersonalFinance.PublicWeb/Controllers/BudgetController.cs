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
//using PersonalFinance.Web.Models;

//namespace PersonalFinance.PublicWeb.Controllers
//{
//    [Authorize]
//    [RoutePrefix("api/budget")]
//    public class BudgetController : ApiController
//    {
//        private AccountManager _accountManager;

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
//        [Route("budgetItems")]
//        public async Task<IHttpActionResult> GetBudgetItems()
//        {

//            IList<BudgetItemViewModel> budgetItems = await AccountManager.GetBudgetItems();

//            var incomes = budgetItems.Where(x => x.Type == "Income");
//            var expenses = budgetItems.Where(x => x.Type == "Expense");

//            var json = new
//            {
//                Incomes = incomes,
//                Expenses = expenses
//            };

//            return Ok(json);
//        }
//    }
//}
