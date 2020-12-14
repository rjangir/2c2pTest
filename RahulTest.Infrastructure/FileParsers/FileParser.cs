using RahulTest.Core.Abstractions.FileParsers;
using System.IO;
using System.Linq;
using RahulTest.Common.Extensions;
using RahulTest.Domain.EF.Core.Entities;
using System.Collections.Generic;

namespace RahulTest.Infrastructure.FileParsers
{
    public class FileParser : IParser
    {

        public IEnumerable<Transaction> ParseValidate(TextReader reader, string format)
        {                                                                                                                                                                                                                                                                   
            switch (format)
            {
                case "csv":
                    var csvParser = new CsvParser();
                    var models = csvParser.Parse(reader);
                    return models.Select(x => new Transaction
                    {
                        Amount = x.Amount,
                        TransactionIdentifier = x.TransactionId,
                        CurrencyCode = x.CurrencyCode,
                        TransactionDate = x.TransactionDate.ToDateTime(CsvParser.DateTimeFormat),
                        Status = x.Status.CsvToTransactionStatus()
                    });
                case "xml":

                    return Enumerable.Empty<Transaction>();

                    break;
                default:
                    return Enumerable.Empty<Transaction>();
            }
        }
    }
}
