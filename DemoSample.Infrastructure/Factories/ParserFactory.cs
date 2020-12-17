using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoSample.Core.Abstractions.Factories;
using DemoSample.Core.Abstractions.FileParsers;
using DemoSample.Infrastructure.FileParsers;

namespace DemoSample.Infrastructure.Factories
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
