using DemoSample.Domain.EF.Core.Entities;
using System.Collections.Generic;
using System.IO;

namespace DemoSample.Core.Abstractions.FileParsers
{
    public interface IParser
    {
        IEnumerable<Transaction> ParseValidate(StreamReader reader);

    }
}