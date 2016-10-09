using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T4TS;

namespace PersonalFinance.Common.Model
{
    [TypeScriptInterface]
    public class PendingInvites
    {
        public string ToEmail { get; set; }
        public DateTime DateSent { get; set; }
    }
}
