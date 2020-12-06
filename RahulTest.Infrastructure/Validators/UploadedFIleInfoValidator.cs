using Microsoft.Extensions.Configuration;
using RahulTest.Core.Abstractions.Validators;
using RahulTest.Core.CustomConfig;
using RahulTest.Core.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RahulTest.Infrastructure.Validators
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
