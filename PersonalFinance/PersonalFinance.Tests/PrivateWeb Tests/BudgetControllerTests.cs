using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Utils.Extensions;
using PersonalFinance.PrivateWeb.Controllers;
using PersonalFinance.PrivateWeb.Database.Repositories;
using PersonalFinance.PrivateWeb.Models;
using PersonalFinance.PrivateWeb.Providers;

namespace PersonalFinance.Tests.PrivateWeb_Tests
{
    [TestFixture]
    public class BudgetControllerTests
    {
        private IUserResolver _userResolver;
        private IBudgetRepository _budgetRepository;
        private IBudgetItemRepository _budgetItemRepository;

        private BudgetController _budgetController;

        private readonly UserDetails _fakeUser = new UserDetails()
        {
            DisplayName = "Fake User",
            Email = "fake@test.com",
            Id = 1,
            IpAddress = "192.168.0.1",
            IsAnonymous = false,
            IsPrivateApiClient = true,
            Roles = new List<Role>() { Role.PublicUser }
        };

        [SetUp]
        public void Setup()
        {
            //Create substituted repos
            _userResolver = Substitute.For<IUserResolver>();
            _budgetRepository = Substitute.For<IBudgetRepository>();
            _budgetItemRepository = Substitute.For<IBudgetItemRepository>();

            //Create new account controller with substituted repos
            _budgetController = new BudgetController(_userResolver, _budgetRepository, _budgetItemRepository);
        }

        [Test]
        public void Test_GetBudgetsForUser()
        {
            //User resolver will return fake user when called
            _userResolver.GetUser().Returns(_fakeUser);
            _budgetRepository.GetBudgetsForUser(1).Returns(new List<Budget>
            {
                new Budget {Id = 1, UserId = 1, Type = BudgetType.Savings.Description(), Name = "Test Budget", AllocatedAmount = 5000, Balance = 200}
            });

            var response = _budgetController.GetBudgetsForUser();

            Assert.AreEqual(response.Result[0].Name, "Test Budget");
        }

        [Test]
        public void Test_GetBudgetItemsForBudget_AccessAnotherUsersBudget()
        {
            //Fake user Id = 1
            _userResolver.GetUser().Returns(_fakeUser);

            //Substitute GetByIdAsync method
            _budgetRepository.GetByIdAsync(1).Returns(new Budget
            {
                Id = 1, UserId = 2, Type = BudgetType.Savings.Description(), Name = "Test Budget", AllocatedAmount = 5000, Balance = 200
            });

            //Substitute GetBudgetItemsForBudget method
            _budgetItemRepository.GetBudgetItemsForBudget(1).Returns(new List<BudgetItem>
            {
                new BudgetItem {Id = 1, BudgetId = 1, Type = BudgetItemType.Expense.Description(), Name = "Test Budget Item", Amount = 5000}
            });

            Assert.That(() => _budgetController.GetBudgetItemsForBudget(1),
                Throws.Exception.TypeOf<UnauthorizedAccessException>());
        }

        [Test]
        public void Test_GetBudgetItemsForBudget_AccessUsersOwnBudget()
        {
            //Fake user Id = 1
            _userResolver.GetUser().Returns(_fakeUser);

            //Substitute GetByIdAsync method
            _budgetRepository.GetByIdAsync(1).Returns(new Budget
            {
                Id = 1,
                UserId = 1,
                Type = BudgetType.Savings.Description(),
                Name = "Test Budget",
                AllocatedAmount = 5000,
                Balance = 200
            });

            //Substitute GetBudgetItemsForBudget method
            _budgetItemRepository.GetBudgetItemsForBudget(1).Returns(new List<BudgetItem>
            {
                new BudgetItem {Id = 1, BudgetId = 1, Type = BudgetItemType.Expense.Description(), Name = "Test Budget Item", Amount = 5000}
            });

            var response = _budgetController.GetBudgetItemsForBudget(1);

            Assert.AreEqual(response.Result[0].Name, "Test Budget Item");
        }

        [Test]
        public void Test_GetRecentBudgetItems()
        {
            //User resolver will return fake user when called
            _userResolver.GetUser().Returns(_fakeUser);
            _budgetRepository.GetMostRecentBudgetForUser(_fakeUser.Id).Returns(new Budget
            {
                Id = 1, UserId = 1, Type = BudgetType.Savings.Description(), Name = "Test Budget", AllocatedAmount = 5000, Balance = 200
            });

            //Substitute GetBudgetItemsForBudget method
            _budgetItemRepository.GetBudgetItemsForBudget(1).Returns(new List<BudgetItem>
            {
                new BudgetItem {Id = 1, BudgetId = 1, Type = BudgetItemType.Expense.Description(), Name = "Test Budget Item", Amount = 5000}
            });

            var response = _budgetController.GetRecentBudgetItems();

            Assert.AreEqual(response.Result.Budget.Name, "Test Budget");
            Assert.AreEqual(response.Result.Items[0].Name, "Test Budget Item");
        }

        [Test]
        public void Test_AddBudget()
        {
            var budget = new Budget
            {
                Id = 1,
                UserId = 2,
                Type = BudgetType.Savings.Description(),
                Name = "Test Budget",
                AllocatedAmount = 5000,
                Balance = 200
            };

            //User resolver will return fake user when called
            _userResolver.GetUser().Returns(_fakeUser);

            var response = _budgetController.AddBudget(budget);

            Assert.IsTrue(response.Result.Succeed);
            Assert.AreEqual(response.Result.Response, "Budget sucessfully added");
        }

        [Test]
        public void Test_DeleteBudgetById_AnotherUsersBudget()
        {
            var budget = new Budget
            {
                Id = 1,
                UserId = 2,
                Type = BudgetType.Savings.Description(),
                Name = "Test Budget",
                AllocatedAmount = 5000,
                Balance = 200
            };

            _userResolver.GetUser().Returns(_fakeUser);

            Assert.That(() => _budgetController.DeleteBudgetById(budget), 
                Throws.Exception.TypeOf<AccessViolationException>());
        }

        [Test]
        public void Test_DeleteBudgetById_UsersOwnBudget()
        {
            var budget = new Budget
            {
                Id = 1,
                UserId = 1,
                Type = BudgetType.Savings.Description(),
                Name = "Test Budget",
                AllocatedAmount = 5000,
                Balance = 200
            };

            _userResolver.GetUser().Returns(_fakeUser);

            var response = _budgetController.DeleteBudgetById(budget);

            Assert.IsTrue(response.Result.Succeed);
            Assert.AreEqual(response.Result.Response, "Budget successfully deleted");
        }
    }
}
