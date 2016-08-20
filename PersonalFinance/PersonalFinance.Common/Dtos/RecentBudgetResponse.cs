﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinance.Common.Model;
using T4TS;

namespace PersonalFinance.Common.Dtos
{
    [TypeScriptInterface]
    public class RecentBudgetResponse
    {
        public Budget Budget { get; set; }
        public List<BudgetItem> Items { get; set; } 
    }
}
