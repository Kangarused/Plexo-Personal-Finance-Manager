using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinance.Common.Utils;

namespace PersonalFinance.Common.Model
{
    [TypescriptEnum]
    public enum Role
    {
        PublicUser,
        HouseholdMember,
        HouseholdAdmin,
        AnonymousOverPrivateApi,
        SystemAdministrator
    }
}
