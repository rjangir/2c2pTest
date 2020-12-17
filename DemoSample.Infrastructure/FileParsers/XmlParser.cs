using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DemoSample.Common.Extensions;
using DemoSample.Core.Abstractions.FileParsers;
using DemoSample.Core.Exceptions;
using DemoSample.Core.ViewModels;
using DemoSample.Domain.EF.Core.Entities;

namespace DemoSample.Infrastructure.FileParsers
{
    public class XmlParser :IParser
    {
        public static string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";

        private readonly XmlSerializer _xmlSerializer;
        public XmlParser()
        {
            _xmlSerializer = new XmlSerializer(typeof(XmlImportModel)); 
        }

        public IEnumerable<Transaction> ParseValidate(StreamReader reader)
        {
            try
            {
                var model = (XmlImportModel)_xmlSerializer.Deserialize(reader);
                return model.XmlTransactionModes.Select(x => new Transaction
                {
                    Amount = x.PaymentDetails.Amount,
                    TransactionIdentifier = x.TransactionId,
                    CurrencyCode = x.PaymentDetails.CurrencyCode,
                    TransactionDate = x.TransactionDate.ToDateTime(DateTimeFormat),
                    Status = ToTransactionStatus(x.Status)
                });
            }
            catch (Exception ex)
            {
                throw new FileParseValidationException($"Xml file validation error: {ex.Message}");
            }
        }

        private static TransactionStatus ToTransactionStatus(string csvStatus)
        {
            switch (csvStatus)
            {
                case "Approved":
                    return TransactionStatus.Approved;
                case "Rejected":
                    return TransactionStatus.Rejected;
                case "Done":
                    return TransactionStatus.Done;
                default:
                    return TransactionStatus.Rejected;
            }
        }
    }
}
