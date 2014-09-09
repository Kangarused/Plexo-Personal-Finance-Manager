using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using PersonalFinance.API.DataAccess;
using PersonalFinance.API.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Insight.Database;

namespace PersonalFinance.API
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            IUserDataAccess userDataAccess = context.Get<SqlConnection>().As<IUserDataAccess>();

            return new ApplicationUserManager(new Infrastructure.UserStore(userDataAccess));
        }
    }
}