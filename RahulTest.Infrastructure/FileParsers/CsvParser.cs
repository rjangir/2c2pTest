using CsvHelper;
using CsvHelper.Configuration;
using RahulTest.Core.Abstractions.FileParsers;
using RahulTest.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace RahulTest.Infrastructure.FileParsers
{
    public class CsvParser
    {
        public static string DateTimeFormat = "dd/MM/yyyy hh:mm:ss";
        public CsvParser()
        {

        }

        public List<CsvTransactionModel> Parse(TextReader reader)
        {

            CsvReader csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            csv.Configuration.Delimiter = ",";
            csv.Configuration.MissingFieldFound = null;

            var listTran = new List<CsvTransactionModel>();
            while (csv.Read())
            {
                try
                {
                    CsvTransactionModel Record = csv.GetRecord<CsvTransactionModel>();
                    listTran.Add(Record);
                }
                catch (Exception ex) 
                {
                    throw;
                }

            }
            return listTran;
        }
    }
}
