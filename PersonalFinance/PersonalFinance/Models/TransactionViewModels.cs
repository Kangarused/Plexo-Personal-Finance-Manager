using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalFinance.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public decimal Reconciled { get; set; }
        public bool isReconciled { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
    }

    public class NewTransactionViewModel
    {
        public int AccountId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Reconciled { get; set; }
        public string Category { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
    }

    public class PagingInfo
    {
        public int accountId { get; set; }
        public int page { get; set; }
        public int itemsPerPage { get; set; }
        public string sortBy { get; set; }
        public bool reverse { get; set; }
    }

    public class SearchTransactionViewModel
    {
        public int accountId { get; set; }
        public string description { get; set; }
        public decimal? minAmount { get; set; }
        public decimal? maxAmount { get; set; }
        public bool? isReconciled { get; set; }
        public string category { get; set; }
        public DateTimeOffset? minDate { get; set; }
        public DateTimeOffset? maxDate { get; set; }
    }
}