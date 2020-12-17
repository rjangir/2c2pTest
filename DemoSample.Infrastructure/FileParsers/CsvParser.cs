using CsvHelper;
using DemoSample.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DemoSample.Core.Exceptions;
using DemoSample.Domain.EF.Core.Entities;
using IParser = DemoSample.Core.Abstractions.FileParsers.IParser;
using DemoSample.Common.Extensions;

namespace DemoSample.Infrastructure.FileParsers
{
    public class CsvParser : IParser
    {
        public static string DateTimeFormat = "dd/MM/yyyy hh:mm:ss";

        public IEnumerable<Transaction> ParseValidate(StreamReader reader)
        {

            CsvReader csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            csv.Configuration.Delimiter = ",";
            csv.Configuration.MissingFieldFound = null;

            var listTran = new List<CsvTransactionModel>();

            try
            {
                while (csv.Read())
                {
                    CsvTransactionModel record = csv.GetRecord<CsvTransactionModel>();
                    listTran.Add(record);
                }
                return listTran.Select(x => new Transaction
                {
                    Amount = x.Amount,
                    TransactionIdentifier = x.TransactionId,
                    CurrencyCode = x.CurrencyCode,
                    TransactionDate = x.TransactionDate.ToDateTime(CsvParser.DateTimeFormat),
                    Status = CsvToTransactionStatus(x.Status)
                });
            }
            catch (Exception ex)
            {
                throw new FileParseValidationException($"Csv file validation error: {ex.Message}");
            }
        }
        private static TransactionStatus CsvToTransactionStatus(string csvStatus)
        {
            return csvStatus switch
            {
                "Approved" => TransactionStatus.Approved,
                "Rejected" => TransactionStatus.Rejected,
                "Finished" => TransactionStatus.Done,
                _ => TransactionStatus.Rejected
            };
        }
    }
}
