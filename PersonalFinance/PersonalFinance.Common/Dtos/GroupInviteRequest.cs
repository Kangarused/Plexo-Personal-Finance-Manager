using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinance.Common.Model;
using T4TS;

namespace PersonalFinance.Common.Dtos
{
    [TypeScriptInterface]
    public class GroupInviteRequest
    {
        public int GroupId { get; set; }
        public string ToUserEmail { get; set; }
        public string InviteMessage { get; set; }
        public Role InviteRole { get; set; }
    }
}
