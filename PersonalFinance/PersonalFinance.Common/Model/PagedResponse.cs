using System.Collections.Generic;

namespace PersonalFinance.Common.Model
{
    public class PagedResponse<T>
    {
        public IList<T> Items { get; set; }
        public long NumRecords { get; set; }
    }
}
