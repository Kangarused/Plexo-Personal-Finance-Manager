using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalFinance.DomainModels
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Reconciled { get; set; }
        public bool isReconciled { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public int Updated_By { get; set; }
        public DateTimeOffset Created_On { get; set; }
        public DateTimeOffset Updated_On { get; set; }
    }
}