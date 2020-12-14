using RahulTest.Domain.EF.Core.Entities;
using System.Collections.Generic;
using System.IO;

namespace RahulTest.Core.Abstractions.FileParsers
{
    public interface IParser
    {
        IEnumerable<Transaction> ParseValidate(TextReader reader,string format);
    }
}