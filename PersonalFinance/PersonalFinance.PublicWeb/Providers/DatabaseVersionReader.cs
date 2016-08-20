using System;
using System.Threading.Tasks;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Providers;

namespace PersonalFinance.PublicWeb.Providers
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