using Insight.Database.Schema;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Database
{
    class Program
    {
        static void Main()
        {
            var schema = new SchemaObjectCollection();
            schema.Load(Assembly.GetExecutingAssembly());

            // automatically create the database
            var connectionString = "server = .; database = PersonalFinance; integrated security = true;";
            SchemaInstaller.CreateDatabase(connectionString);

            // automatically install it, or upgrade it
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var installer = new SchemaInstaller(connection);
                new SchemaEventConsoleLogger().Attach(installer);
                installer.Install("PersonalFinance", schema);
            }
        }
    }
}
