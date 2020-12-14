using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RahulTest.Core.Abstractions.FileParsers;
using RahulTest.Core.Abstractions.Services;
using RahulTest.Core.Abstractions.Validators;
using RahulTest.Core.Exceptions;
using RahulTest.Domain.EF.Core.Entities;
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
    public class TransactionsController : ControllerBase
    {
        private readonly IUploadedFIleInfoValidator uploadedFIleInfoValidator;
        private readonly IParser parser;
        private readonly ITransactionsService _transactionsService;
        public TransactionsController(IUploadedFIleInfoValidator fIleInfoValidator, IParser fileParser, ITransactionsService transactionsService)
        {
            uploadedFIleInfoValidator = fIleInfoValidator;
            _transactionsService = transactionsService;
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
                    uploadedFIleInfoValidator.Validate(format, file.Length);

                    var result = new StringBuilder();

                    using (var reader = new StreamReader(file.OpenReadStream()) as TextReader)
                    {
                        var transactionsModel = parser.ParseValidate(reader, format);
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
                catch(Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

                }
            }

            return BadRequest("No input file");
        }

        [Route("getByid/{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                return Ok(_transactionsService.GetById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [Route("getByDate/{date}")]
        public IActionResult getByDate(string date)
        {
            try
            {
                if (!string.IsNullOrEmpty(date))
                {
                    var dateTime = DateTime.Parse(date);
                    return Ok(_transactionsService.GetByDateRange(dateTime));
                }
                else
                {
                    return BadRequest("Incorrect Date format");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [Route("getByStatus/{status}")]
        public IActionResult getByStatus(TransactionStatus status)
        {
            try
            {
                return Ok(_transactionsService.GetByStatus(status));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
