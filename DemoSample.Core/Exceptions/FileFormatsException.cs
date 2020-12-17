
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSample.Core.Exceptions
{
    public class InvalidFileFormatException : Exception
    {
        public InvalidFileFormatException(string message) : base(message)
        {

        }
    }
}
