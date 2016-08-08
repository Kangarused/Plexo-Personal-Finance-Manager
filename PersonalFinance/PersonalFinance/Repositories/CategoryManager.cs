using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using PersonalFinance.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Insight.Database;
using System.Threading.Tasks;
using PersonalFinance.DomainModels;

namespace PersonalFinance.Repositories
{
    public class CategoryManager : IDisposable
    {
        private readonly ICategoryDataAccess _categoryData;

        public CategoryManager(ICategoryDataAccess categoryData)
        {
            _categoryData = categoryData;
        }

        public static CategoryManager Create(IdentityFactoryOptions<CategoryManager> options, IOwinContext context)
        {
            ICategoryDataAccess categoryData = context.Get<SqlConnection>().As<ICategoryDataAccess>();
            CategoryManager manager = new CategoryManager(categoryData);

            return manager;
        }



        public void Dispose()
        {
            // dispose of stuff here.
        }
    }
}