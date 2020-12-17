
using DemoSample.Domain.EF.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSample.Core.Dtos
{
    public class TransactionDto
    {
        public string Id { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }

        public static string GetStatus(TransactionStatus transactionStatus)
        {
            switch (transactionStatus)
            {
                case TransactionStatus.Approved:
                    return "A";
                case TransactionStatus.Rejected:
                default:
                    return "R";
                case TransactionStatus.Done:
                    return "D";
            }
        }

    }
}
