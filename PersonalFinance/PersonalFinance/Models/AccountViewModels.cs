using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalFinance.Models
{
    public class NewAccountViewModel
    {
        public string Name { get; set; }
    }

    public class AccountDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal Reconciled { get; set; }
        public bool isReconciled { get; set; }
    }
}