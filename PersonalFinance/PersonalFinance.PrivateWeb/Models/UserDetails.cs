using System.Collections.Generic;
using System.Linq;
using PersonalFinance.Common.Model;
using T4TS;

namespace PersonalFinance.PrivateWeb.Models
{
    [TypeScriptInterface]
    public class UserDetails
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public List<Role> Roles { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsPrivateApiClient { get; set; }
        public string IpAddress { get; set; }

        public bool IsSystemAdmin
        {
            get
            {
                if (Roles == null || Roles.Count == 0)
                    return false;
                return Roles.Contains(Role.SystemAdministrator);
            }
        }
    }
}
