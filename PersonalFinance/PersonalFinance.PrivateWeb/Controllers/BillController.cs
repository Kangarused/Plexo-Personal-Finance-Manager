using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PersonalFinance.Common.Model;
using PersonalFinance.PrivateWeb.Database.Repositories;
using PersonalFinance.PrivateWeb.Providers;

namespace PersonalFinance.PrivateWeb.Controllers
{
    public class BillController : ApiController
    {
        private readonly IUserResolver _userResolver;
        private readonly IUserRepository _userRepository;
        private readonly IBillRepository _billRepository;

        public BillController(
            IUserResolver userResolver,
            IUserRepository userRepository,
            IBillRepository billRepository
            )
        {
            _userResolver = userResolver;
            _userRepository = userRepository;
            _billRepository = billRepository;
        }

        [AcceptVerbs("GET")]
        public async Task<List<Bill>> GetBills()
        {
            var currentUser = _userResolver.GetUser();
            var bills = await _billRepository.GetBillsForUser(currentUser.Id);

            return bills;
        }

        [AcceptVerbs("GET")]
        public async Task<Bill> GetBillById(int param)
        {
            var currentUser = _userResolver.GetUser();
            var bill = await _billRepository.GetByIdAsync(param);

            if (bill.UserId == currentUser.Id)
            {
                return bill;
            }

            throw new AccessViolationException("User does not have access to that account");
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<string>> CreateBill(Bill newBill)
        {
            var currentUser = _userResolver.GetUser();
            newBill.UserId = currentUser.Id;

            //todo: add validation

            await _billRepository.InsertAsync(newBill);

            return new ActionResponseGeneric<string>()
            {
                Succeed = true,
                Response = "Bill created successfully"
            };


            //return new ActionResponseGeneric<string>()
            //{
            //    Succeed = false,
            //    Response = "Failed to Create: Validation Issues"
            //};
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<string>> DeleteBill([FromBody]int param)
        {
            var bill = await _billRepository.GetByIdAsync(param);
            var currentUser = _userResolver.GetUser();

            if (bill.UserId == currentUser.Id)
            {
                await _billRepository.DeleteAsync(x => x.Id == param);
                return new ActionResponseGeneric<string>()
                {
                    Succeed = true,
                    Response = "Bill deleted successfully"
                };
            }

            throw new AccessViolationException("User does not have access to that account");
        }
    }
}