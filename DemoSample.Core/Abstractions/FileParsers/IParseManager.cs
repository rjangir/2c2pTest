using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DemoSample.Domain.EF.Core.Entities;

namespace DemoSample.Core.Abstractions.FileParsers
{
    public interface IParseManager
    {
        IEnumerable<Transaction> ParseValidate(StreamReader reader,string format);

    }
}
