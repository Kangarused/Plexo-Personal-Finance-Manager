using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Insight.Database;
using PersonalFinance.DomainModels;
using System.Threading.Tasks;

namespace PersonalFinance.DataAccess
{
    [Sql(Schema = "dbo")]
    public interface ICategoryDataAccess
    {
        Task<int> InsertCategoryAsync(Category category);

        Task<IList<Category>> GetCategoriesAsync();
    }
}