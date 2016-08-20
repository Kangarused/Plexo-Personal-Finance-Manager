using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Providers;

namespace PersonalFinance.PrivateWeb.Providers
{
    [Singleton]
    public class DatabaseVersionReader : IDatabaseVersionReader
    {
        //todo:implement this
        public Task<long> GetDatabaseVersionAsync()
        {
            return Task.Run(() => Convert.ToInt64(0));
        }
    }
}