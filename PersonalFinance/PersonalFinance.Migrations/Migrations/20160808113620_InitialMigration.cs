using DOB.SkilledMigration.Migrations;
using FluentMigrator;

namespace PersonalFinance.Migrations.Migrations
{
    [Migration(20160808113620)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithId()
                .WithColumn("UserName").AsString().NotNullable();
        }

        public override void Down()
        {
            Execute.DropTableIfExists("Users");
        }
    }
}
