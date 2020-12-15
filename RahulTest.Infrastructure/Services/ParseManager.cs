using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RahulTest.Core.Abstractions.Factories;
using RahulTest.Core.Abstractions.FileParsers;
using RahulTest.Domain.EF.Core.Entities;

namespace RahulTest.Infrastructure.Services
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
