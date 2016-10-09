using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T4TS;

namespace PersonalFinance.Common.Dtos
{
    [TypeScriptInterface]
    public class CreateGroupRequest
    {
        public string Name { get; set; }
    }
}
