using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalFinance.Models
{
    public class BudgetItemViewModel
    {
        public int Id { get; set; }
        public Guid Household { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public int AnnualFrequency { get; set; }
    }
}