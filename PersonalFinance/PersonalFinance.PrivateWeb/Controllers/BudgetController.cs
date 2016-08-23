using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PersonalFinance.Common.Dtos;
using PersonalFinance.Common.Model;
using PersonalFinance.PrivateWeb.Database.Repositories;
using PersonalFinance.PrivateWeb.Providers;

namespace PersonalFinance.PrivateWeb.Controllers
{
    public class BudgetController : ApiController
    {
        private readonly IUserResolver _userResolver;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IBudgetItemRepository _budgetItemRepository;

        public BudgetController(
            IUserResolver userResolver,
            IBudgetRepository budgetRepository,
            IBudgetItemRepository budgetItemRepository
            )
        {
            _userResolver = userResolver;
            _budgetRepository = budgetRepository;
            _budgetItemRepository = budgetItemRepository;
        }

        [AcceptVerbs("GET")]
        public async Task<Budget> GetBudgetById(int param)
        {
            var budget = await _budgetRepository.GetByIdAsync(param);
            var currentUser = _userResolver.GetUser();

            if (budget.UserId == currentUser.Id)
            {
                return budget;
            }

            throw new AccessViolationException("Attempted access of a budget belonging to another user");
        }

        [AcceptVerbs("GET")]
        public async Task<BudgetDetailsResponse> GetBudgetDetailsById(int param)
        {
            var budget = await _budgetRepository.GetByIdAsync(param);
            var currentUser = _userResolver.GetUser();

            if (budget.UserId == currentUser.Id)
            {
                var budgetItems = await _budgetItemRepository.GetBudgetItemsForBudget(param);
                return new BudgetDetailsResponse
                {
                    Budget = budget,
                    BudgetItems = budgetItems
                };
            }

            throw new AccessViolationException("Attempted access of a budget belonging to another user");
        }

        [AcceptVerbs("GET")]
        public async Task<List<Budget>> GetBudgetsForUser()
        {
            var currentUser = _userResolver.GetUser();
            var budgets = await _budgetRepository.GetBudgetsForUser(currentUser.Id);

            return budgets;
        }

        [AcceptVerbs("GET")]
        public async Task<List<BudgetItem>> GetBudgetItemsForBudget(int budgetId)
        {
            var currentUser = _userResolver.GetUser();
            var budget = await _budgetRepository.GetByIdAsync(budgetId);

            if (budget.UserId == currentUser.Id)
            {
                var budgetItems = await _budgetItemRepository.GetBudgetItemsForBudget(budgetId);
                return budgetItems;
            }

            throw new UnauthorizedAccessException("Attempted access of unowned budget");
        }

        [AcceptVerbs("GET")]
        public async Task<RecentBudgetResponse> GetRecentBudgetItems()
        {
            var currentUser = _userResolver.GetUser();
            var recentBudget = await _budgetRepository.GetMostRecentBudgetForUser(currentUser.Id);
            var items = await _budgetItemRepository.GetBudgetItemsForBudget(recentBudget.Id);

            return new RecentBudgetResponse
            {
                Budget = recentBudget,
                Items = items
            };
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<string>> AddBudget(Budget budget)
        {
            var currentUser = _userResolver.GetUser();

            budget.UserId = currentUser.Id;
            budget.CreatedBy = currentUser.DisplayName;
            budget.CreatedTime = DateTime.Now;
            budget.ModifiedBy = currentUser.DisplayName;
            budget.ModifiedTime = DateTime.Now;

            if (budget.Type == BudgetType.Savings.ToString())
            {
                budget.Balance = 0;
            }
            else
            {
                budget.Balance = budget.AllocatedAmount;
            }

            var result = await _budgetRepository.InsertAsync(budget);

            return new ActionResponseGeneric<string>
            {
                Succeed = true,
                Response = "Budget sucessfully added"
            };
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<string>> DeleteBudgetById(Budget budget)
        {
            var currentUser = _userResolver.GetUser();

            if (budget.UserId == currentUser.Id)
            {
                await _budgetItemRepository.DeleteAsync(x => x.BudgetId == budget.Id, false);
                await _budgetRepository.DeleteAsync(x => x.Id == budget.Id, false);

                return new ActionResponseGeneric<string>
                {
                    Succeed = true,
                    Response = "Budget successfully deleted"
                };
            }

            throw new AccessViolationException("Attempted removal of a budget belonging to another user");
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<string>> AddBudgetItem(BudgetItem budgetItem)
        {
            //todo: validate fields serverside

            var currentUser = _userResolver.GetUser();
            var budget = await _budgetRepository.GetByIdAsync(budgetItem.BudgetId);

            if (budget.UserId == currentUser.Id)
            {
                var result = await _budgetItemRepository.InsertAsync(budgetItem);
                await UpdateBudgetBalance(budget, budgetItem);

                return new ActionResponseGeneric<string>
                {
                    Succeed = true,
                    Response = "Budget transaction sucessfully added"
                };
            }
            throw new AccessViolationException("Attempting to save budget transaction to a budget not owned by current user");
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<string>> DeleteBudgetItem(BudgetItem budgetItem)
        {
            //todo: validate fields serverside

            var currentUser = _userResolver.GetUser();
            var budget = await _budgetRepository.GetByIdAsync(budgetItem.BudgetId);

            if (budget.UserId == currentUser.Id)
            {
                await _budgetItemRepository.DeleteAsync(x => x.Id == budgetItem.Id, false);
                await UpdateBudgetBalance(budget, budgetItem);

                return new ActionResponseGeneric<string>
                {
                    Succeed = true,
                    Response = "Budget transaction sucessfully deleted"
                };
            }
            throw new AccessViolationException("Attempting to delete a budget transaction from a budget not owned by current user");
        }

        private async Task<bool> UpdateBudgetBalance(Budget budget, BudgetItem budgetItem)
        {
            if (budgetItem.Type == BudgetItemType.Expense.ToString())
            {
                budget.Balance -= budgetItem.Amount;
            }
            else
            {
                budget.Balance += budgetItem.Amount;
            }

            await _budgetRepository.UpdateAsync(budget);
            return true;
        }
    }
}