using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSample.Core.Exceptions
{
    public class MaxFileSizeException :Exception
    {
        public MaxFileSizeException(string message):base(message)
        {

        }
    }
}
