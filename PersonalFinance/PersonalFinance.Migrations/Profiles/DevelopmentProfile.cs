using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using FluentMigrator.Runner.Extensions;

namespace DOB.SkilledMigration.Migrations.Profiles
{
    [Profile("Development")]
    public class DevelopmentProfile : Migration
    {
        public override void Up()
        {

        }

        public override void Down()
        {
            
        }
    }
}