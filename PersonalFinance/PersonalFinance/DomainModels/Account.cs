using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalFinance.DomainModels
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Household { get; set; }
        public string Name { get; set; }
        public bool isReconciled { get; set; }
        public DateTimeOffset Created_On { get; set; }
        public DateTimeOffset Updated_On { get; set; }
    }
}