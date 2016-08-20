using System;

namespace PersonalFinance.Migrations.Profiles.MockData
{
    public class MockAudit
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}
