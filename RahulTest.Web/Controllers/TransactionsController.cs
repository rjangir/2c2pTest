using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RahulTest.Core.Abstractions.FileParsers;
using RahulTest.Core.Abstractions.Validators;
using RahulTest.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RahulTest.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionsController :ControllerBase
    {
        private readonly IUploadedFIleInfoValidator uploadedFIleInfoValidator;
        private readonly IParser parser;
        public TransactionsController(IUploadedFIleInfoValidator fIleInfoValidator,IParser fileParser)
        {
            uploadedFIleInfoValidator = fIleInfoValidator;
            parser = fileParser;

        }

        [Route("import")]
        public async Task<IActionResult> OnUpload(IList<IFormFile> files)
        {

            if (files.Any())
            {
                var file = files[0];

                try
                {
                    var format = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                    uploadedFIleInfoValidator.Validate(format,file.Length);

                    var result = new StringBuilder();
                    
                    using (var reader = new StreamReader(file.OpenReadStream()) as TextReader)
                    {
                        parser.Parse(reader, format);
                        while (reader.Peek() >= 0)
                            result.AppendLine(await reader.ReadLineAsync());
                    }

                }
                catch (InvalidFileFormatException ex )
                {
                    return BadRequest(ex.Message);
                }
                catch (MaxFileSizeException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok();
        }
    }
}
