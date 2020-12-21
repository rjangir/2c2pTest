using DemoSample.Core.Abstractions.FileParsers;
using DemoSample.Core.Abstractions.Services;
using DemoSample.Core.Abstractions.Validators;
using DemoSample.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSample.Web.Controllers
{
    [ApiController]
    [Route("import-api")]
    public class TransactionsImportController :ControllerBase
    {
        private readonly IUploadedFIleInfoValidator _uploadedFIleInfoValidator;
        private readonly IParseManager _parser;
        private readonly ITransactionsService _transactionsService;
        public TransactionsImportController(IUploadedFIleInfoValidator fIleInfoValidator, IParseManager fileParser, ITransactionsService transactionsService)
        {
            _uploadedFIleInfoValidator = fIleInfoValidator;
            _transactionsService = transactionsService;
            _parser = fileParser;

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
                    _uploadedFIleInfoValidator.Validate(format, file.Length);

                    var result = new StringBuilder();

                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        var transactionsModel = _parser.ParseValidate(reader, format);
                        if (transactionsModel.Any())
                        {
                            await _transactionsService.SaveTransactions(transactionsModel.ToList());
                            return Ok();

                        }
                        else
                        {
                            return BadRequest("No records found");
                        }

                    }
                }
                catch (InvalidFileFormatException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (MaxFileSizeException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (FileParseValidationException fpex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, fpex.Message);

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

                }
            }

            return BadRequest("No input file");
        }
    }
}
