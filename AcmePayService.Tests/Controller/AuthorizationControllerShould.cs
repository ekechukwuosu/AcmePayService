using AcmePayService.API.Controllers;
using AcmePayService.Common.Helper;
using AcmePayService.Domain.Command;
using AcmePayService.Domain.Command.Requests;
using AcmePayService.Domain.Command.Responses;
using AcmePayService.Domain.Queries;
using AcmePayService.Domain.Queries.Responses;
using AcmePayService.Tests.Utilities.TestParameters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AcmePayService.Tests.Controller
{
    public class AuthorizationControllerShould
    {
        private readonly AuthorizationController _authorizationController;
        private Mock<IMediator> _mockIMediator = new Mock<IMediator>();
        public AuthorizationControllerShould() 
        {
            _authorizationController = new AuthorizationController(_mockIMediator.Object);
        }
        [Fact]
        public void Get_All_Transactions_Should_return_Ok_Success()
        {
            var request = TestRequestParameters.GetSampleTransactionRequest();

            _mockIMediator.Setup(s => s.Send(It.IsAny<TransactionQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ServiceResponse<TransactionResponse>());

            var result = _authorizationController.GetAllTransactions(request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        [Fact]
        public void Authorize_Payment_Should_return_Ok_Success()
        {
            var request = TestRequestParameters.SampleAuthorizePaymentRequest();

            _mockIMediator.Setup(s => s.Send(It.IsAny<AuthorizePaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ServiceResponse<PaymentResponse>());

            var result = _authorizationController.AuthorizePayment(request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        [Fact]
        public void Authorize_Payment_Should_return_bad_request()
        {
            var request = new AuthorizeRequest();

            _mockIMediator.Setup(s => s.Send(It.IsAny<AuthorizePaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ServiceResponse<PaymentResponse>());

            var result = _authorizationController.AuthorizePayment(request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public void Capture_Payment_Should_return_Ok_Success()
        {
            var request = TestRequestParameters.SampleCapturePaymentRequest();
            var id = TestRequestParameters.SampleExistingID();

            _mockIMediator.Setup(s => s.Send(It.IsAny<CapturePaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ServiceResponse<PaymentResponse>());

            var result = _authorizationController.CapturePayment(id, request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        [Fact]
        public void Capture_Payment_Should_return_bad_request()
        {
            var request = new CaptureAndVoidInputRequest();

            _mockIMediator.Setup(s => s.Send(It.IsAny<CapturePaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ServiceResponse<PaymentResponse>());

            var result = _authorizationController.CapturePayment(new Guid(), request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public void Void_Payment_Should_return_Ok_Success()
        {
            var request = TestRequestParameters.SampleCapturePaymentRequest();
            var id = TestRequestParameters.SampleExistingID();

            _mockIMediator.Setup(s => s.Send(It.IsAny<VoidPaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ServiceResponse<PaymentResponse>());

            var result = _authorizationController.VoidPayment(id, request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        [Fact]
        public void Void_Payment_Should_return_bad_request()
        {
            var request = new CaptureAndVoidInputRequest();

            _mockIMediator.Setup(s => s.Send(It.IsAny<VoidPaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ServiceResponse<PaymentResponse>());

            var result = _authorizationController.VoidPayment(new Guid(), request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
    }
}
