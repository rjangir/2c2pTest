using Microsoft.Extensions.Configuration;
using DemoSample.Core.Abstractions.Validators;
using DemoSample.Core.CustomConfig;
using DemoSample.Core.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoSample.Infrastructure.Validators
{
    public class UploadedFIleInfoValidator : IUploadedFIleInfoValidator
    {
        private static IEnumerable<string> AllowedFormats;
        private static int MaxSizeGB;
        public UploadedFIleInfoValidator(IConfiguration configuration)
        {
            UploadFIleSection section = new UploadFIleSection
            {
                AllowedFormats = configuration["UploadFile:AllowedFormats"],
                MaxFileSize = int.Parse(configuration["UploadFile:MaxFileSize"])
            };
            AllowedFormats = section.AllowedFormats.Split(",");
            MaxSizeGB = section.MaxFileSize;
        }

        public void Validate(string format, long fileSizeBytes)
        {
            if (!AllowedFormats.Contains(format))
                throw new InvalidFileFormatException("Unknown format");
            if (fileSizeBytes / (1024 * 1024) > MaxSizeGB)
                throw new Exception("File size not allowed");
        }
    }
}
