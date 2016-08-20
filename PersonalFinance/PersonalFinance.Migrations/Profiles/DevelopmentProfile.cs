using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using FluentMigrator.Runner.Extensions;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using PersonalFinance.Common.Utils.Extensions;
using PersonalFinance.Migrations.Profiles.MockData;

namespace PersonalFinance.Migrations.Profiles
{
    [Profile("Development")]
    public class DevelopmentProfile : Migration
    {
        private readonly List<MockBudget> _budgets = new List<MockBudget>();
        private readonly MockAudit _mockAudit = new MockAudit
        {
            CreatedBy = "migrations",
            CreatedTime = DateTime.Now,
            ModifiedBy = "migrations",
            ModifiedTime = DateTime.Now
        }; 

        public override void Up()
        {
            LoadUsers();
            LoadBudgets();
            LoadBudgetItems();
            LoadAuthClients();
        }

        private void LoadUsers()
        {
            Insert.IntoTable("Users").WithIdentityInsert().Row(new
            {
                Id = 1,
                Name = "Test User",
                UserName = "Test",
                Email = "test@test.com",
                PhoneNumber = "123456789",
                PasswordHash = new CryptoProvider(null).GetHash("welcome"),
                _mockAudit.CreatedBy,
                _mockAudit.CreatedTime,
                _mockAudit.ModifiedBy,
                _mockAudit.ModifiedTime
            });

            Insert.IntoTable("UserRoles").Row(new
            {
                UserId = 1,
                Role = Role.PublicUser
            });
        }

        private void LoadBudgets()
        {
            Random rand = new Random();

            for (int i = 1; i <= 4; i++)
            {
                var trigger = rand.Next(1, 10);

                _budgets.Add(new MockBudget
                {
                    Id = i,
                    UserId = 1,
                    Name = Faker.Lorem.Sentence(),
                    AllocatedAmount = Faker.RandomNumber.Next(100, 10000),
                    Balance = 0,
                    Type = (trigger < 5) ? BudgetType.Savings : BudgetType.Spendings
                });

                Insert.IntoTable("Budgets").WithIdentityInsert().Row(new
                {
                    _budgets[i-1].Id,
                    _budgets[i-1].Name,
                    _budgets[i-1].UserId,
                    _budgets[i-1].Type,
                    _budgets[i-1].AllocatedAmount,
                    _budgets[i-1].Balance,
                    _mockAudit.CreatedBy,
                    _mockAudit.CreatedTime,
                    _mockAudit.ModifiedBy,
                    _mockAudit.ModifiedTime
                });
            }
        }

        private void LoadBudgetItems()
        {
            Random rand = new Random();

            foreach (MockBudget budget in _budgets)
            {
                for (int i = 0; i < 10; i++)
                {
                    var trigger = rand.Next(1, 10);

                    Insert.IntoTable("BudgetItems").Row(new
                    {
                        BudgetId = budget.Id,
                        Type = (trigger < 5) ? BudgetItemType.Expense : BudgetItemType.Income,
                        Name = String.Join(" ", Faker.Lorem.Words(3)),
                        Description = Faker.Lorem.Sentence(),
                        Amount = Faker.RandomNumber.Next(10, 50)
                    });
                }
            }
        }


        private void LoadAuthClients()
        {
            Insert.IntoTable("AuthClient").Row(new
            {
                Name = "websiteAuth",
                Secret = new CryptoProvider(null).GetHash("SHRE%fZy4RL@8vButG#*%^KP6#yK6p"),
                ApplicationType = "AngularJS front-end Application",
                Active = true,
                AllowedOrigin = "http://localhost:2053"
            });
        }

        public override void Down()
        {
            Delete.FromTable("AuthClient").AllRows();
        }
    }
}