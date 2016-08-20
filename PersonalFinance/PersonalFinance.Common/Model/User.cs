using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using T4TS;

namespace PersonalFinance.Common.Model
{
    [TypeScriptInterface]
    public partial class User
    {
        [Reference]
        public List<UserRole> UserRoles { get; set; }

        [Reference]
        public List<UserLogin> UserLogins { get; set; }
    }
}
