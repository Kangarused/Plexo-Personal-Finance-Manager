using System;
using System.ComponentModel;
using PersonalFinance.Migrations.Extensions;
using FluentMigrator;
using FluentMigrator.Expressions;

namespace PersonalFinance.Migrations.Migrations
{
    [Migration(20160808113620)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("AuthClient")
                .WithId()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Secret").AsMaxString().NotNullable()
                .WithColumn("ApplicationType").AsString().NotNullable()
                .WithColumn("Active").AsBoolean().WithDefaultValue(0).NotNullable()
                .WithColumn("AllowedOrigin").AsString().NotNullable();

            Create.Table("Users")
                .WithId()
                .WithColumn("UserName").AsString().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("PhoneNumber").AsString().Nullable()
                .WithColumn("PasswordHash").AsMaxString().Nullable().WithAuditInfo();

            Create.Index("UQ_Users_UserName").OnTable("Users").OnColumn("UserName").Unique();
            Create.Index("UQ_Users_Email").OnTable("Users").OnColumn("Email").Unique();

            Create.Table("UserRoles")
                .WithId()
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id").NotNullable()
                .WithColumn("Role").AsString().NotNullable();

            Create.UniqueConstraint("UQ_UserRoles").OnTable("UserRoles").Columns("UserId", "Role");

            Create.Table("Audit")
                .WithId()
                .WithColumn("EntityType").AsString(255).NotNullable()
                .WithColumn("Key").AsInt32().NotNullable()
                .WithColumn("Operation").AsString(255).NotNullable()
                .WithColumn("Serialised").AsMaxString().NotNullable()
                .WithColumn("User").AsString(255).NotNullable()
                .WithColumn("Ip").AsString(255).NotNullable()
                .WithColumn("TransactionId").AsInt32().Nullable()
                .WithColumn("DateTime").AsDateTime().NotNullable();

            Create.Table("Households")
                .WithId()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Address").AsString().Nullable()
                .WithColumn("PhoneNumber").AsString().Nullable();

            Create.Table("HouseholdMembers")
                .WithId()
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id").NotNullable()
                .WithColumn("HouseholdId").AsInt32().ForeignKey("Households", "Id").NotNullable();

            Create.Table("Budgets")
                .WithId()
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id").Nullable()
                .WithColumn("HouseholdId").AsInt32().ForeignKey("Households", "Id").Nullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithAuditInfo();

            Create.Table("BudgetItems")
                .WithId()
                .WithColumn("BudgetId").AsInt32().ForeignKey("Budgets", "Id").NotNullable()
                .WithColumn("Type").AsString().Nullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsMaxString().Nullable()
                .WithColumn("Amount").AsCurrency().NotNullable()
                .WithColumn("AnnualFrequency").AsInt32().Nullable();

            Create.Table("Account")
                .WithId()
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id").Nullable()
                .WithColumn("HouseholdId").AsInt32().ForeignKey("Households", "Id").Nullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("IsReconciled").AsBoolean().WithDefaultValue(0).NotNullable()
                .WithAuditInfo();

            Create.Table("Transactions")
                .WithId()
                .WithColumn("AccountId").AsInt32().ForeignKey("Account", "Id").NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsMaxString().Nullable()
                .WithColumn("Amount").AsCurrency().NotNullable()
                .WithColumn("ReconciledAmount").AsCurrency().NotNullable()
                .WithColumn("IsReconciled").AsBoolean().WithDefaultValue(0).NotNullable()
                .WithColumn("TransactionDate").AsDateTime().NotNullable()
                .WithAuditInfo();

            Create.Table("Billing")
                .WithId()
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id").Nullable()
                .WithColumn("HouseholdId").AsInt32().ForeignKey("Households", "Id").Nullable();

            Create.Table("Bill")
                .WithId()
                .WithColumn("BillingId").AsInt32().ForeignKey("Billing", "Id").NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsMaxString().Nullable()
                .WithColumn("DueDate").AsDateTime().NotNullable()
                .WithColumn("Amount").AsCurrency().NotNullable()
                .WithColumn("AnnualFrequency").AsInt32().Nullable()
                .WithColumn("IsPaid").AsBoolean().WithDefaultValue(0).NotNullable();

            Create.Table("Categories")
                .WithId()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsMaxString().Nullable();

            Create.Table("BudgetItemCategories")
                .WithId()
                .WithColumn("CategoryId").AsInt32().ForeignKey("Categories", "Id").NotNullable()
                .WithColumn("BudgetItemId").AsInt32().ForeignKey("BudgetItems", "Id").NotNullable();

            Create.Table("TransactionCategories")
                .WithId()
                .WithColumn("CategoryId").AsInt32().ForeignKey("Categories", "Id").NotNullable()
                .WithColumn("TransactionId").AsInt32().ForeignKey("Transactions", "Id").NotNullable();

            Create.Table("HouseholdInvites")
                .WithId()
                .WithColumn("FromUserId").AsInt32().ForeignKey("Users", "Id")
                .WithColumn("ToUserId").AsInt32().ForeignKey("Users", "Id")
                .WithColumn("Pending").AsBoolean().WithDefaultValue(1).NotNullable()
                .WithColumn("Accepted").AsBoolean().WithDefaultValue(0).NotNullable()
                .WithColumn("DateSent").AsDateTime().NotNullable()
                .WithColumn("DateAccepted").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Execute.DropTableIfExists("HouseholdInvites");
            Execute.DropTableIfExists("TransactionCategories");
            Execute.DropTableIfExists("BudgetItemCategories");
            Execute.DropTableIfExists("Categories");
            Execute.DropTableIfExists("Bill");
            Execute.DropTableIfExists("Billing");
            Execute.DropTableIfExists("Transactions");
            Execute.DropTableIfExists("Account");
            Execute.DropTableIfExists("BudgetItems");
            Execute.DropTableIfExists("Budgets");
            Execute.DropTableIfExists("HouseholdMembers");
            Execute.DropTableIfExists("Households");
            Execute.DropTableIfExists("Audit");
            Execute.DropTableIfExists("UserRoles");
            Execute.DropTableIfExists("Users");
            Execute.DropTableIfExists("AuthClient");
        }
    }
}
