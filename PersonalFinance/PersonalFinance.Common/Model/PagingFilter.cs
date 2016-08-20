using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T4TS;

namespace PersonalFinance.Common.Model
{
    [TypeScriptInterface]
    public class PagingFilter
    {
        //manual switch if you need the full result set.
        public bool DisablePaging { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public string OrderColumn { get; set; }
        public OrderDirection OrderDirection { get; set; }
    }

    public enum OrderDirection
    {
        ASC,
        DESC
    }
}
