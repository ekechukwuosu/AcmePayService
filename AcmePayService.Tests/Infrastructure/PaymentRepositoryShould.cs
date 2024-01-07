using AcmePayService.Common;
using AcmePayService.Common.Helper;
using AcmePayService.Domain.Entities;
using AcmePayService.Domain.Enums;
using AcmePayService.Domain.Models;
using AcmePayService.Infrastructure.RepositoryImplementation;
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
            Assert.IsAssignableFrom<ServiceResponse<PagedList<PaymentReportDTO>>>(result);
            Assert.True(expectedResponse.Equals(actualResponse));
        }
        [Fact]
        public async void ActionPayment_Return_Authorized()
        {
            var parameters = TestRequestParameters.SampleAuthorizePaymentRequestForActionPayment();
            var result = await _paymentRepository.AddPayment(PaymentStatus.Authorized, parameters);;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ServiceResponse<Payment>>(result);
            Assert.True(result.Status.Equals("Authorized"));
        }

        [Fact]
        public async void ActionPayment_Return_Captured()
        {
            var parameters = TestRequestParameters.SampleCapturePaymentRequestForActionPayment();
            var result = await _paymentRepository.UpdatePayment(PaymentStatus.Captured, parameters); ;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ServiceResponse<Payment>>(result);
            Assert.True(result.Status.Equals("Captured"));
        }
        [Fact]
        public async void ActionPayment_Return_Voided()
        {
            var parameters = TestRequestParameters.SampleVoidedPaymentRequestForActionPayment();
            var result = await _paymentRepository.UpdatePayment(PaymentStatus.Voided, parameters); ;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ServiceResponse<Payment>>(result);
            Assert.True(result.Status.Equals("Voided"));
        }        
    }
}
