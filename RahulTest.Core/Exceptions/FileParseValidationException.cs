using System;
using System.Collections.Generic;
using System.Text;

namespace RahulTest.Core.Exceptions
{
    public class FileParseValidationException : Exception
    {
        public FileParseValidationException(string message):base(message)
        {
            
        }
    }
}
