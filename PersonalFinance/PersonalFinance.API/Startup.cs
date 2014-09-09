﻿using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(PersonalFinance.API.Startup))]
namespace PersonalFinance.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            app.CreatePerOwinContext<SqlConnection>(() => new SqlConnection(connectionString));
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config); // Register the Routes
            app.UseWebApi(config);
        }
    }
}