using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Insight.Database;
using PersonalFinance.API.DomainModels;
using System.Threading.Tasks;

namespace PersonalFinance.API.DataAccess
{
    [Sql(Schema = "dbo")]
    public interface ICategoryDataAccess
    {
        Task<int> InsertCategoryAsync(Category category);

        Task<IList<Category>> GetCategoriesAsync();
    }
}