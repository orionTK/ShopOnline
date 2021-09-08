using ShopOnline.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int ExternalTransactionId { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public Status? Status { get; set; }
        public string Provider { set; get; }

        public Guid UserId { get; set; }

        //public AppUser AppUser { get; set; }
    }
}
