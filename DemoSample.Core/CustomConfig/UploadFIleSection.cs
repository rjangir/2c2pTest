using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSample.Core.CustomConfig
{
   public class UploadFIleSection
    {
        public int MaxFileSize { get; set; }
       // public string MaxFileSize { get; set; }
        public string AllowedFormats { get; set; }
    }
}
