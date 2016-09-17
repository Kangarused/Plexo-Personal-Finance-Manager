using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PersonalFinance.Common.Model;
using PersonalFinance.PublicWeb.Providers;

namespace PersonalFinance.PublicWeb.Controllers
{
    public class BillController : ApiController
    {
        private readonly IPrivateApiProvider _privateApiProvider;

        public BillController(IPrivateApiProvider privateApiProvider)
        {
            _privateApiProvider = privateApiProvider;
        }

        [AcceptVerbs("GET")]
        public Task<List<Bill>> GetBills()
        {
            return _privateApiProvider.InvokeGetAsync<List<Bill>>("Bill", "GetBills");
        }

        [AcceptVerbs("GET")]
        public Task<Bill> GetBillById(int param)
        {
            return _privateApiProvider.InvokeGetAsync<Bill>("Bill", "GetBillById", param);
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> CreateBill(Bill param)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<string>>("Bill", "CreateBill", param);
        }

        [AcceptVerbs("POST")]
        public Task<ActionResponseGeneric<string>> DeleteBill([FromBody]int param)
        {
            return _privateApiProvider.InvokePostAsync<ActionResponseGeneric<string>>("Bill", "DeleteBill", param);
        }
    }
}