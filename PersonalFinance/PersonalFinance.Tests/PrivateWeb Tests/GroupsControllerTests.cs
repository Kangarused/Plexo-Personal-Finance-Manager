using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class GroupsControllerTests
    {
        private IAccountRepository _accountRepository;
        private IUserResolver _userResolver;
        private IUserRepository _userRepository;

        private GroupController _groupController;

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
        public async Task Test_LeaveGroup_EnsureGroupIsDeleted()
        {
            int groupId = 1;

            GroupMember member = new GroupMember()
            {
                GroupId = 1,
                UserId = _fakeUser.Id,
                IsAdmin = false
            };

            _userResolver.GetUser().Returns(_fakeUser);
            _groupMemberRepository.GetUser(_fakeUser.Id).Returns(member);
            var result = await _groupController.LeaveGroup(groupId);

            Assert.IsTrue(result.Success);

            Assert.That(() => _groupController.GetByIdAsync(groupId),
                Throws.Exception.TypeOf<InvalidOperationException>());

            Assert.That(() => _budgetController.GetBudgetForGroup(groupId),
                Throws.Exception.TypeOf<InvalidOperationException>());

            Assert.That(() => _billController.GetBillForGroup(groupId),
                Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public async Task Test_LeaveGroup_EnsureGroupIsDeleted()
        {
            int budgetId = 1;

            GroupMember member = new GroupMember()
            {
                GroupId = 1,
                UserId = _fakeUser.Id,
                IsAdmin = false
            };

            _userResolver.GetUser().Returns(_fakeUser);
            _groupMemberRepository.GetUser(_fakeUser.Id).Returns(member);

            Assert.That(() => _budgetController.DeleteBudget(budgetId),
                Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public async Task Test_Acce_EnsureGroupIsDeleted()
        {
            int budgetId = 1;

            GroupMember member = new GroupMember()
            {
                GroupId = 1,
                UserId = _fakeUser.Id,
                IsAdmin = false
            };

            _userResolver.GetUser().Returns(_fakeUser);
            _groupMemberRepository.GetUser(_fakeUser.Id).Returns(member);

            Assert.That(() => _budgetController.DeleteBudget(budgetId),
                Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public async Task Test_CreateGroup_TextFieldLimitCharacters()
        {
            string randomString = "MJK7MvlWjNpqXCKRCP57rqIpwOXcCu90oVpOiPVRhLp" +
                                  "evYQSZ6zZ86mGTwHLWb0CRSIWBmA9pKXOQlBEuo4QDG" +
                                  "gfX98ZIKnkLAuJ7EMQGRrkbSFgHDkKixNcNYU4TDuHN" +
                                  "vKHxuxzEBSQlgG5KwNcD2mYGEq06aTkNSrShUSlNVHs" +
                                  "779U8taCWQFveaEoNfWUOQ2zH4AeJpBoY1zDEDHDbqA" +
                                  "rbQrACPEs89AbHLGk6UVkWrO1Hb9Qhw6QpHoHAZf7fb" +
                                  "mF4kTA4bvb749zIAbJ3YjNIkq6pp5c90zX1jIMxirVZ" +
                                  "6jXVr7QPXNzWYii1qq0ll9VjPBqGJVA1Em2LlfvgZx1" +
                                  "oJqpiFeJHAMPNGRwc7wogniE1HBmKtwc7Q4qtk9gi6h" +
                                  "YpDkIiU8Nq0BlaV52ZBAcFubYWwUqY12aunDYPaStUQ" +
                                  "QgV7PyAsQDL3KYeuqsYJVU6fjyA8Tf3a6Vvi3wena3W" +
                                  "6sxyg6HBhFLWgz2uiYSV8hMmZv8";
            int stringLength = randomString.Length;

            Group newGroup = new Group()
            {
                UserId = _fakeUser.Id,
                Name = randomString;
            };

            _userResolver.GetUser().Returns(_fakeUser);
            int groupId = await _groupController.CreateGroup(newGroup);

            var result = await _groupController.GetByIdAsync(groupId);
            Assert.AreNotEqual(result.Name, newGroup.Name);
            Assert.AreEqual(result.Name.Length, 255);
        }
    }
}
