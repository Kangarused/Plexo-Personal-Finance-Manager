﻿using System.Web;
using System.Web.SessionState;

namespace PersonalFinance.PrivateWeb
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_PostAuthorizeRequest()
        {
            if (HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api"))
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }
    }
}
