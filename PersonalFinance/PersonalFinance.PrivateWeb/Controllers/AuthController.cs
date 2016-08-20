using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PersonalFinance.Common.Model;
using PersonalFinance.PrivateWeb.Database.Repositories;
using PersonalFinance.PrivateWeb.WebApiFilters;

namespace PersonalFinance.PrivateWeb.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IAuthClientRepository _authClientRepository;

        public AuthController(IAuthClientRepository authClientRepository)
        {
            _authClientRepository = authClientRepository;
        }


        [WebApiFilters.Authorize(Role.AnonymousOverPrivateApi)]
        [AcceptVerbs("GET")]
        public Task<AuthClient> GetAuthClient(string param)
        {
            return _authClientRepository.GetAuthClientByName(param);
        }
    }
}