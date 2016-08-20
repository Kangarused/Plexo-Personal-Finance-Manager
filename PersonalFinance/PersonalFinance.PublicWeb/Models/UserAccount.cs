using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using PersonalFinance.Common.Model;
using T4TS;


namespace PersonalFinance.PublicWeb.Models
{
    [TypeScriptInterface]
    public class UserAccount
    {
        public string Provider { get; set; }
        public string ExternalAccessToken { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Role Role { get; set; }
        public bool IsUserExternalAuthenticated => !Provider.IsNullOrWhiteSpace();
    }
}