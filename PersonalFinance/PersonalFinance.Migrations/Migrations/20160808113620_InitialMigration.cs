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

            Create.Table("Groups")
                .WithId()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("GroupMembers")
                .WithId()
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id").NotNullable()
                .WithColumn("GroupId").AsInt32().ForeignKey("Groups", "Id").NotNullable()
                .WithColumn("Role").AsString().NotNullable();

            Create.Table("Budgets")
                .WithId()
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id").Nullable()
                .WithColumn("GroupId").AsInt32().ForeignKey("Groups", "Id").Nullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Type").AsString().NotNullable()
                .WithColumn("AllocatedAmount").AsCurrency().NotNullable()
                .WithColumn("Balance").AsCurrency().NotNullable()
                .WithAuditInfo();

            Create.Table("BudgetItems")
                .WithId()
                .WithColumn("BudgetId").AsInt32().ForeignKey("Budgets", "Id").NotNullable()
                .WithColumn("Type").AsString().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsMaxString().Nullable()
                .WithColumn("Amount").AsCurrency().NotNullable();

            Create.Table("Bill")
                .WithId()
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id").Nullable()
                .WithColumn("GroupId").AsInt32().ForeignKey("Groups", "Id").Nullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsMaxString().Nullable()
                .WithColumn("DueDate").AsDateTime().NotNullable()
                .WithColumn("Amount").AsCurrency().NotNullable()
                .WithColumn("AnnualFrequency").AsInt32().Nullable()
                .WithColumn("Status").AsString().NotNullable();

            Create.Table("GroupInvites")
                .WithId()
                .WithColumn("FromUserId").AsInt32().ForeignKey("Users", "Id")
                .WithColumn("ToUserId").AsInt32().ForeignKey("Users", "Id")
                .WithColumn("GroupId").AsInt32().ForeignKey("Groups", "Id")
                .WithColumn("Pending").AsBoolean().WithDefaultValue(1).NotNullable()
                .WithColumn("Accepted").AsBoolean().WithDefaultValue(0).NotNullable()
                .WithColumn("DateSent").AsDateTime().NotNullable()
                .WithColumn("DateAccepted").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Execute.DropTableIfExists("GroupInvites");
            Execute.DropTableIfExists("Bill");
            Execute.DropTableIfExists("Billing");
            Execute.DropTableIfExists("BudgetItems");
            Execute.DropTableIfExists("Budgets");
            Execute.DropTableIfExists("GroupMembers");
            Execute.DropTableIfExists("Groups");
            Execute.DropTableIfExists("Audit");
            Execute.DropTableIfExists("UserRoles");
            Execute.DropTableIfExists("Users");
            Execute.DropTableIfExists("AuthClient");
        }
    }
}
