using AcmePayService.API.Controllers;
using AcmePayService.Common;
using AcmePayService.Domain.DTO;
using AcmePayService.Domain.Models;
using AcmePayService.Services.Command.AuthorizePayment;
using AcmePayService.Services.Command.CapturePayment;
using AcmePayService.Services.Command.Requests;
using AcmePayService.Services.Command.VoidPayment;
using AcmePayService.Services.Queries.Transaction;
using AcmePayService.Tests.Utilities.TestParameters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AcmePayService.Tests.Controller
{
    public class PaymentProcessorControllerShould
    {
        private readonly PaymentProcessorController _authorizationController;
        private Mock<IMediator> _mockIMediator = new Mock<IMediator>();
        public PaymentProcessorControllerShould() 
        {
            _authorizationController = new PaymentProcessorController(_mockIMediator.Object);
        }
        [Fact]
        public void Get_All_Transactions_Should_return_Ok_Success()
        {
            var request = TestRequestParameters.GetSampleTransactionRequest();

            _mockIMediator.Setup(s => s.Send(It.IsAny<TransactionQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new TransactionResponse(new PagedList<PaymentReportDTO>(new List<PaymentReportDTO>(),0,0,0)));

            var result = _authorizationController.GetAllTransactions(request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        [Fact]
        public void Authorize_Payment_Should_return_Ok_Success()
        {
            var request = TestRequestParameters.SampleAuthorizePaymentRequest();

            _mockIMediator.Setup(s => s.Send(It.IsAny<AuthorizePaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PaymentDTO());

            var result = _authorizationController.AuthorizePayment(request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        [Fact]
        public void Authorize_Payment_Should_return_bad_request()
        {
            var request = new AuthorizeRequest();

            _mockIMediator.Setup(s => s.Send(It.IsAny<AuthorizePaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PaymentDTO());

            var result = _authorizationController.AuthorizePayment(request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public void Capture_Payment_Should_return_Ok_Success()
        {
            var request = TestRequestParameters.SampleCapturePaymentRequest();
            var id = TestRequestParameters.SampleExistingID();

            _mockIMediator.Setup(s => s.Send(It.IsAny<CapturePaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PaymentDTO());

            var result = _authorizationController.CapturePayment(id, request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        [Fact]
        public void Capture_Payment_Should_return_bad_request()
        {
            var request = new CaptureAndVoidInputRequest();

            _mockIMediator.Setup(s => s.Send(It.IsAny<CapturePaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PaymentDTO());

            var result = _authorizationController.CapturePayment(new Guid(), request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public void Void_Payment_Should_return_Ok_Success()
        {
            var request = TestRequestParameters.SampleCapturePaymentRequest();
            var id = TestRequestParameters.SampleExistingID();

            _mockIMediator.Setup(s => s.Send(It.IsAny<VoidPaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PaymentDTO());

            var result = _authorizationController.VoidPayment(id, request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        [Fact]
        public void Void_Payment_Should_return_bad_request()
        {
            var request = new CaptureAndVoidInputRequest();

            _mockIMediator.Setup(s => s.Send(It.IsAny<VoidPaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PaymentDTO());

            var result = _authorizationController.VoidPayment(new Guid(), request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
    }
}
