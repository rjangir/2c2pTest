using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DemoSample.Core.Abstractions.Factories;
using DemoSample.Core.Abstractions.FileParsers;
using DemoSample.Domain.EF.Core.Entities;

namespace DemoSample.Infrastructure.Services
{
    public class ParseManager :IParseManager
    {
        private readonly IParserFactory _parserFactory;
        public ParseManager(IParserFactory parserFactory)
        {
            _parserFactory = parserFactory;
        }
        public IEnumerable<Transaction> ParseValidate(StreamReader reader, string format)
        {
            var parser = _parserFactory.GetParser(format);
            return parser.ParseValidate(reader);
        }
    }
}
