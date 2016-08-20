using System;

namespace PersonalFinance.Common.Model
{
    public interface IEntityWithTimeStamp
    {
        DateTime CreatedTime { get; set; }
        DateTime ModifiedTime { get; set; }
    }
}
