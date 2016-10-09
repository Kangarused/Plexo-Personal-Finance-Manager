using System;
using NSubstitute;
using NUnit.Framework;
using PersonalFinance.Common.Model;
using PersonalFinance.PrivateWeb.Controllers;
using PersonalFinance.PrivateWeb.Database.Repositories;

namespace PersonalFinance.Tests.PrivateWeb_Tests
{
    [TestFixture]
    public class UserAccountControllerTests
    {
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;

        private UserAccountController _userAccountController;

        private readonly User _fakeUser = new User
        {
            Id = 1,
            Name = "Fake User",
            Email = "fake@test.com",
            PhoneNumber = "1234567890",
            PasswordHash = "hash",
            CreatedBy = "test",
            CreatedTime = DateTime.Now,
            ModifiedTime = DateTime.Now,
            ModifiedBy = "test"
        };

        [SetUp]
        public void Setup()
        {
            //Create substituted repos
            _userRoleRepository = Substitute.For<IUserRoleRepository>();
            _userRepository = Substitute.For<IUserRepository>();

            //Create new account controller with substituted repos
            _userAccountController = new UserAccountController(_userRepository, _userRoleRepository);
        }

        [Test]
        public void Test_GetUser_Exists()
        {
            _userRepository.LoadByIdAsync(1).Returns(_fakeUser);

            var response = _userAccountController.GetUser(1);

            Assert.AreEqual(response.Result.Name, "Fake User");
        }

        [Test]
        public void Test_GetUser_NotExists()
        {
            var response = _userAccountController.GetUser(2);

            Assert.IsTrue(response.Result == null);
        }

        [Test]
        public void Test_GetUserByEmail_Exists()
        {
            _userRepository.LoadByEmailAsync(_fakeUser.Email).Returns(_fakeUser);

            var response = _userAccountController.GetUserByEmail(_fakeUser.Email);

            Assert.AreEqual(response.Result.Name, "Fake User");
        }

        [Test]
        public void Test_GetUserByUsername_Exists()
        {
            _userRepository.LoadByUsernameAsync(_fakeUser.UserName).Returns(_fakeUser);

            var response = _userAccountController.GetUserByUsername(_fakeUser.UserName);

            Assert.AreEqual(response.Result.Name, "Fake User");
        }

        [Test]
        public void Test_CreateAccountWithSimplePassword()
        {
            string simplePassword = "1234";

            User testUser = new User
            {
                Name = "Test User",
                UserName = "Test Account",
                Email = "testAccount@test.com",
                PasswordHash = simplePassword
            };

            var response = _userAccountController.AddUser(testUser);

            Assert.AreEqual(response.Result.Response, 
                "Password is not strong enough, must be atleast 8 characters" +
                " long and include atleast 1 number and 1 uppercase letter");
        }
    }
}
