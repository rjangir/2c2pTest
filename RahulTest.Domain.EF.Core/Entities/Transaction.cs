using System;
using System.Collections.Generic;
using System.Text;

namespace RahulTest.Domain.EF.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public string TransactionIdentifier { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatus Status { get; set; }
    }

    public enum TransactionStatus
    {
        Approved = 1,
        Rejected,
        Done
    }
}
