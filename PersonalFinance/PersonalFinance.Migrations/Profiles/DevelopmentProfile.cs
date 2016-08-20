using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using FluentMigrator.Runner.Extensions;
using PersonalFinance.Common.Providers;

namespace PersonalFinance.Migrations.Profiles
{
    [Profile("Development")]
    public class DevelopmentProfile : Migration
    {
        public override void Up()
        {
            LoadAuthClients();
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