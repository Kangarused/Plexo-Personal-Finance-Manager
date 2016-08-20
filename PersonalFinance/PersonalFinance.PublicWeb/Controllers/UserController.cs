using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PersonalFinance.Common.Model;
using PersonalFinance.PublicWeb.Providers;

namespace PersonalFinance.PublicWeb.Controllers
{
    public class UserController : ApiController
    {
        private readonly IPrivateApiProvider _privateApiProvider;

        public UserController(IPrivateApiProvider privateApiProvider)
        {
            _privateApiProvider = privateApiProvider;
        }

        [AcceptVerbs("GET")]
        public async Task<User> GetUserById(int param)
        {
            var user = await _privateApiProvider.InvokeGetAsync<User>("User", "GetUserById", param);
            user.PasswordHash = null;
            return user;
        }
    }
}