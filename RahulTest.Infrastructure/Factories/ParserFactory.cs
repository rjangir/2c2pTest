using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RahulTest.Core.Abstractions.Factories;
using RahulTest.Core.Abstractions.FileParsers;
using RahulTest.Infrastructure.FileParsers;

namespace RahulTest.Infrastructure.Factories
{
    public class ParserFactory :IParserFactory
    {
        public IParser GetParser(string format)
        {
            switch (format)
            {
                case "csv":
                    return new CsvParser();
                case "xml":
                    return new XmlParser();
                default:
                    return null;
            }
        }
    }
}
