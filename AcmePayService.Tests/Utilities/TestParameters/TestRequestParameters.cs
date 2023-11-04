using AcmePayService.Domain.Command.Requests;
using AcmePayService.Domain.Queries.Requests;
using AcmePayService.Infrastructure.Data.Models;

namespace AcmePayService.Tests.Utilities.TestParameters
{
    public class TestRequestParameters
    {
        public static TransactionRequest GetSampleTransactionRequest()
        {
            return new TransactionRequest()
            {
                PageNumber = 1,
                PageSize = 2               
            };
        }
        
        public static AuthorizeRequest SampleAuthorizePaymentRequest()
        {
            return new AuthorizeRequest()
            {
                Amount = 5000,
                Currency = "USD",
                CardHolderNumber = "336598********4587",
                HolderName = "Peter Obi",
                ExpirationMonth = 7,
                ExpirationYear = 29,
                CVV = 121,
                OrderReference = "REF-1111125"
            };
        }
        public static CaptureAndVoidInputRequest SampleCapturePaymentRequest()
        {
            return new CaptureAndVoidInputRequest()
            {                
                OrderReference = "REF-111113",                
            };
        }
        public static CaptureAndVoidInputRequest SampleVoidedRequest()
        {
            return new CaptureAndVoidInputRequest()
            {
                OrderReference = "REF-111114",
            };
        }
        public static Payment SampleAuthorizePaymentRequestForActionPayment()
        {
            return new Payment()
            {
                Amount = 5000,
                Currency = "USD",
                CardHolderNumber = "336598********4587",
                HolderName = "Peter Obi",
                ExpirationMonth = 7,
                ExpirationYear = 29,
                CVV = 121,
                OrderReference = "REF-1111125"
            };
        }
        public static Payment SampleAuthorizePaymentRequestForActionPayment_Exists()
        {
            return new Payment()
            {
                Amount = 5000,
                Currency = "USD",
                CardHolderNumber = "336598********4587",
                HolderName = "Peter Obi",
                ExpirationMonth = 7,
                ExpirationYear = 29,
                CVV = 121,
                OrderReference = "REF-111112"
            };
        }
        public static Payment SampleAuthorizePaymentRequestForActionPayment_NOExists()
        {
            return new Payment()
            {
                Id = new Guid(),
                Amount = 5000,
                Currency = "USD",
                CardHolderNumber = "336598********4587",
                HolderName = "Peter Obi",
                ExpirationMonth = 7,
                ExpirationYear = 29,
                CVV = 121,
                OrderReference = "REF-11111252"
            };
        }
        public static Payment SampleCapturePaymentRequestForActionPayment()
        {
            return new Payment()
            {
                Id = new Guid("D4D44D28-CFC7-496D-A072-3B46CBE8AA44"),
                OrderReference = "REF-111113"
            };
        }
        public static Payment SampleVoidedPaymentRequestForActionPayment()
        {
            return new Payment()
            {
                Id = new Guid("513B6D20-C649-4AF5-AD3B-3FEE5704173C"),
                OrderReference = "REF-111114"
            };
        }
        public static Guid SampleExistingID()
        {
            return  new Guid("513B6D20-C649-4AF5-AD3B-3FEE5704173C");
        }
    }
}


