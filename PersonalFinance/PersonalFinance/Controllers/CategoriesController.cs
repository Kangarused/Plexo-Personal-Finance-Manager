using PersonalFinance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using PersonalFinance.DomainModels;

namespace PersonalFinance.Controllers
{   
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private AccountManager _accountManager;

        public AccountManager AccountManager
        {
            get
            {
                return _accountManager = HttpContext.Current.GetOwinContext().Get<AccountManager>();
            }
            set
            {
                _accountManager = value;
            }
        }

        [HttpGet]
        [Route]
        public async Task<IEnumerable<string>> GetCategories(string value)
        {
            IList<Category> categories = await AccountManager.GetCategories();
            
            var ret = new List<string>();
            foreach (var category in categories.Select(c => c.Name).ToList<string>())
            {
                if (category.ToLower().Contains(value.ToLower()))
                    ret.Add(category);
            }

            return ret;
        }
        
    }
}
