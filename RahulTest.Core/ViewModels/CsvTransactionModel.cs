using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RahulTest.Core.ViewModels
{
    public class CsvTransactionModel
    {
        [MaxLength(50)]
        public string TransactionId { get; set; }
        public decimal Amount  { get; set; }
        public string CurrencyCode { get; set; }
        public string TransactionDate { get; set; }
        public string Status { get; set; }

    }

    enum CsvTransactionStatus
    {

    }
}
