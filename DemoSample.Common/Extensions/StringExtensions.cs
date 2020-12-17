using DemoSample.Domain.EF.Core.Entities;
using System;
using System.Globalization;
using System.Text;

namespace DemoSample.Common.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this string dateString,string format)
        {
            return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
        }

        public static TransactionStatus CsvToTransactionStatus(this string csvStatus)
        {
            switch (csvStatus)
            {
                case "Approved":
                    return TransactionStatus.Approved;
                case "Rejected":
                    return TransactionStatus.Rejected;
                case "Done":
                default:
                    return TransactionStatus.Done;
            }
        }
    }
}
