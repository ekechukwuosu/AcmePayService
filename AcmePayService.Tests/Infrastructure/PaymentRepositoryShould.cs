using AcmePayService.Common.Helper;
using AcmePayService.Common.Static;
using AcmePayService.Domain.Command.Responses;
using AcmePayService.Infrastructure.DAL.Repository.Implementation;
using AcmePayService.Infrastructure.Data.Enums;
using AcmePayService.Infrastructure.Data.Models;
using AcmePayService.Tests.Utilities.FakeDBContext;
using AcmePayService.Tests.Utilities.TestParameters;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace AcmePayService.Tests.Infrastructure
{
    public class PaymentRepositoryShould
    {
        private readonly PaymentRepository _paymentRepository;
        public PaymentRepositoryShould() 
        {
            var _appDBContext = new Mock<FakeDBContexts>();
            var _logger = new Mock<ILogger<PaymentRepository>>();
            _paymentRepository = new PaymentRepository(_appDBContext.Object.GetDatabaseContext().Result, _logger.Object);
        }
        [Fact]
        public async void GetPayments_Should_Return_All_Payments_By_Pagination()
        {
            
            var result = await _paymentRepository.GetPayments(1, 2);

            var expectedResponse = JsonConvert.SerializeObject(TestResponses.GetSamplePaymentList());
            var actualResponse = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ServiceResponse<List<PaymentReport>>>(result);
            Assert.True(expectedResponse.Equals(actualResponse));
        }
        [Fact]
        public async void ActionPayment_Return_Authorized()
        {
            var parameters = TestRequestParameters.SampleAuthorizePaymentRequestForActionPayment();
            var result = await _paymentRepository.ActionPayment(PaymentStatus.Authorized, parameters);;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ServiceResponse<Payment>>(result);
            Assert.True(result.Data.Status.Equals("Authorized"));
        }

        [Fact]
        public async void ActionPayment_Return_Captured()
        {
            var parameters = TestRequestParameters.SampleCapturePaymentRequestForActionPayment();
            var result = await _paymentRepository.ActionPayment(PaymentStatus.Captured, parameters); ;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ServiceResponse<Payment>>(result);
            Assert.True(result.Data.Status.Equals("Captured"));
        }
        [Fact]
        public async void ActionPayment_Return_Voided()
        {
            var parameters = TestRequestParameters.SampleVoidedPaymentRequestForActionPayment();
            var result = await _paymentRepository.ActionPayment(PaymentStatus.Voided, parameters); ;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ServiceResponse<Payment>>(result);
            Assert.True(result.Data.Status.Equals("Voided"));
        }
        [Fact]
        public async void ActionPayment_Authorize_Order_Reference_Exists_Return_ErrorMessage()
        {
            var parameters = TestRequestParameters.SampleAuthorizePaymentRequestForActionPayment_Exists();
            var result = await _paymentRepository.ActionPayment(PaymentStatus.Authorized, parameters); ;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ServiceResponse<Payment>>(result);
            Assert.True(result.ResponseMessage.Equals(ResponseMessages.OrderRefExistsKey));
        }
        [Fact]
        public async void ActionPayment_Capture_Order_Reference_NotExists_Return_ErrorMessage()
        {
            var parameters = TestRequestParameters.SampleAuthorizePaymentRequestForActionPayment_NOExists();
            var result = await _paymentRepository.ActionPayment(PaymentStatus.Captured, parameters); ;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ServiceResponse<Payment>>(result);
            Assert.True(result.ResponseMessage.Equals(ResponseMessages.PaymentNoMatchKey));
        }
        [Fact]
        public async void ActionPayment_Voided_Order_Reference_NotExists_Return_ErrorMessage()
        {
            var parameters = TestRequestParameters.SampleAuthorizePaymentRequestForActionPayment_NOExists();
            var result = await _paymentRepository.ActionPayment(PaymentStatus.Voided, parameters); ;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ServiceResponse<Payment>>(result);
            Assert.True(result.ResponseMessage.Equals(ResponseMessages.PaymentNoMatchKey));
        }
    }
}
