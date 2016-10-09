using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using PersonalFinance.Common.Model;
using PersonalFinance.PrivateWeb.Controllers;
using PersonalFinance.PrivateWeb.Database.Repositories;
using PersonalFinance.PrivateWeb.Models;
using PersonalFinance.PrivateWeb.Providers;

namespace PersonalFinance.Tests.PrivateWeb_Tests
{
    [TestFixture]
    public class AccountsControllerTests
    {
        private IAccountRepository _accountRepository;
        private IUserResolver _userResolver;
        private IUserRepository _userRepository;

        private AccountController _accountController;

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
            _accountRepository = Substitute.For<IAccountRepository>();
            _userRepository = Substitute.For<IUserRepository>();

            //Create new account controller with substituted repos
            _accountController = new AccountController(_userResolver, _userRepository, _accountRepository);
        }

        [Test]
        public void Test_GetAccountById_AccessAnotherUsersAccount()
        {
            //User resolver will return fake user when called
            _userResolver.GetUser().Returns(_fakeUser);
            _accountRepository.GetByIdAsync(Arg.Any<int>()).Returns(new Group()
            {
                Id = 1,
                UserId = 2,
                Name = "Fake Account"
            });

            Assert.That(() => _accountController.GetAccountById(_fakeUser.Id),
                Throws.Exception.TypeOf<AccessViolationException>());
        }

        [Test]
        public void Test_GetAccountById_AccessUsersOwnAccount()
        {
            //User resolver will return fake user when called
            _userResolver.GetUser().Returns(_fakeUser);
            _accountRepository.GetByIdAsync(Arg.Any<int>()).Returns(new Group()
            {
                Id = 1,
                UserId = 1,
                Name = "Fake Account"
            });

            var response = _accountController.GetAccountById(_fakeUser.Id);

            Assert.AreEqual(response.Result.Name, "Fake Account");
        }

        [Test]
        public void Test_CreateAccount_ForAnotherUser()
        {
            //User resolver will return fake user when called
            _userResolver.GetUser().Returns(_fakeUser);
            var newAccount = new Group
            {
                Id = 1,
                UserId = 2,
                Name = "Fake Account",
                Balance = 200,
                Reconciled = 200,
                CreatedBy = "test",
                CreatedTime = DateTime.Now,
                ModifiedBy = "test",
                ModifiedTime = DateTime.Now
            };

            var response = _accountController.CreateAccount(newAccount);

            Assert.IsFalse(response.Result.Succeed);
            Assert.AreEqual(response.Result.Response, "Account creation failed");
        }

        [Test]
        public void Test_CreateAccount_ForCurrentUser()
        {
            //User resolver will return fake user when called
            _userResolver.GetUser().Returns(_fakeUser);
            var newAccount = new Group
            {
                Id = 1,
                UserId = 1,
                Name = "Fake Account",
                Balance = 200,
                Reconciled = 200,
                CreatedBy = "test",
                CreatedTime = DateTime.Now,
                ModifiedBy = "test",
                ModifiedTime = DateTime.Now
            };

            var response = _accountController.CreateAccount(newAccount);

            Assert.IsTrue(response.Result.Succeed);
            Assert.AreEqual(response.Result.Response, "Account created successfully");
        }

        [Test]
        public void Test_DeleteAccount_ForAnotherUser()
        {
            var newAccount = new Group
            {
                Id = 1,
                UserId = 2,
                Name = "Fake Account",
                Balance = 200,
                Reconciled = 200,
                CreatedBy = "test",
                CreatedTime = DateTime.Now,
                ModifiedBy = "test",
                ModifiedTime = DateTime.Now
            };

            _userResolver.GetUser().Returns(_fakeUser);
            _accountRepository.GetByIdAsync(Arg.Any<int>()).Returns(newAccount);


            Assert.That(() => _accountController.DeleteAccount(newAccount.Id),
                Throws.Exception.TypeOf<AccessViolationException>());
        }

        [Test]
        public void Test_DeleteAccount_ForCurrentUser()
        {
            var newAccount = new Group
            {
                Id = 1,
                UserId = 1,
                Name = "Fake Account",
                Balance = 200,
                Reconciled = 200,
                CreatedBy = "test",
                CreatedTime = DateTime.Now,
                ModifiedBy = "test",
                ModifiedTime = DateTime.Now
            };

            _userResolver.GetUser().Returns(_fakeUser);
            _accountRepository.GetByIdAsync(Arg.Any<int>()).Returns(newAccount);

            var response = _accountController.DeleteAccount(newAccount.Id);

            Assert.IsTrue(response.Result.Succeed);
            Assert.AreEqual(response.Result.Response, "Account deleted successfully");
        }
    }
}
