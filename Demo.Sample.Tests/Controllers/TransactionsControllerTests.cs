using DemoSample.Core.Abstractions.Services;
using DemoSample.Core.Dtos;
using DemoSample.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Demo.Sample.Tests.Controllers
{
    public class TransactionsControllerTests
    {
        private readonly Mock<ITransactionsService> _mockService;
        private readonly TransactionsController _controller;
        public TransactionsControllerTests()
        {
            _mockService = new Mock<ITransactionsService>();
            _controller = new TransactionsController(_mockService.Object);
        }

        [Fact]
        public void GetById_ValidId_ReturnsTransaction()
        {
            //arrange
            var id = "inv-001";
            //_mockService.Setup(s => s.GetById(id)).Returns(new TransactionDto { Id = "inv-001", Payment = "200.00 USD", Status = "A" });

            //act
            var result = _controller.GetById(id);
            
            
            //assert
            var actionRresult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<TransactionDto>(actionRresult.Value);

            Assert.Equal(id, model.Id);

        }
    }
}
