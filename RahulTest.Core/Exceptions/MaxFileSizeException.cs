using System;
using System.Collections.Generic;
using System.Text;

namespace RahulTest.Core.Exceptions
{
    public class MaxFileSizeException :Exception
    {
        public MaxFileSizeException(string message):base(message)
        {

        }
    }
}
