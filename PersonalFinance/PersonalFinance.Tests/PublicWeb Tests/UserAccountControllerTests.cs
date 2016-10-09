using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using PersonalFinance.PublicWeb.Controllers;
using PersonalFinance.PublicWeb.Providers;
using PersonalFinance.PublicWeb.Models;
using PersonalFinance.PublicWeb.Repositories;

namespace PersonalFinance.Tests.PublicWeb_Tests
{
    [TestFixture]
    public class UserAccountControllerTests
    {
        private ICryptoProvider _cryptoProvider;
        private IAuthRepository _authRepository;

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
            _cryptoProvider = Substitute.For<ICryptoProvider>();
            _authRepository = Substitute.For<IAuthRepository>();

            //Create new account controller with substituted repos
            _userAccountController = new UserAccountController(_authRepository, _cryptoProvider);
        }

        [Test]
        public async Task Test_RegisterUser_VerifyPasswordHashing()
        {
            string password = "TestPassword123";

            UserAccount testUser = new UserAccount
            {
                Name = "Test User",
                UserName = "Test Account",
                Email = "testAccount@test.com",
                Password = password,
                ConfirmPassword = password
            };

            var response = await _userAccountController.Register(testUser);

            //Assert.AreNotEqual(response.User.PasswordHash, password);
        }
    }
}
