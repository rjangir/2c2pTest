using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSample.Core.Exceptions
{
    public class FileParseValidationException : Exception
    {
        public FileParseValidationException(string message):base(message)
        {
            
        }
    }
}
