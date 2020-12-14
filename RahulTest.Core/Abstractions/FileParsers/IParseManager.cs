using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RahulTest.Domain.EF.Core.Entities;

namespace RahulTest.Core.Abstractions.FileParsers
{
    public interface IParseManager
    {
        IEnumerable<Transaction> ParseValidate(StreamReader reader,string format);

    }
}
