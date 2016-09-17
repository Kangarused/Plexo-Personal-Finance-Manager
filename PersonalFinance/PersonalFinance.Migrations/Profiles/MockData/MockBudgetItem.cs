using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinance.Common.Model;

namespace PersonalFinance.Migrations.Profiles.MockData
{
    public class MockBudgetItem
    {
        public int BudgetId { get; set; }
        public BudgetItemType Type { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
