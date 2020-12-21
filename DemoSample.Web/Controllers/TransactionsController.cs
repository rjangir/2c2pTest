using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoSample.Core.Abstractions.Services;
using DemoSample.Domain.EF.Core.Entities;
using System;

namespace DemoSample.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
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

        [Route("getByCurrency/{code}")]
        public IActionResult GetByCurrency(string code)
        {
            try
            {
                return Ok(_transactionsService.GetByCurrency(code));
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
