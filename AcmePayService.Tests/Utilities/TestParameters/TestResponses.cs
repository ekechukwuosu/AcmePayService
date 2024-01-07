using AcmePayService.Common.Helper;
using AcmePayService.Domain.Entities;
using AcmePayService.Domain.Models;

namespace AcmePayService.Tests.Utilities.TestParameters
{
    public class TestResponses
    {
        public static ServiceResponse<List<PaymentReportDTO>> GetSamplePaymentList()
        {
            var list =  new List<PaymentReportDTO>()
            {
                new PaymentReportDTO(5000,
                    "USD",
                    "336598********4587",
                    "Peter Obi",
                    new Guid("6804C5AF-A163-4836-BDFA-8F0BC6295911"),
                    "Authorized"),
               
                new PaymentReportDTO(6500,
                    "GBP",
                    "962548********3215",
                    "Chinargorom Osu",
                    new Guid("D4D44D28-CFC7-496D-A072-3B46CBE8AA44"),
                    "Captured")
            };
            return new ServiceResponse<List<PaymentReportDTO>>() { Data = list, ResponseMessage = string.Empty };
        }
        public static ServiceResponse<Payment> GetSampleAuthorizedPaymentResponse_Repository()
        {
            var response = new Payment()
            {
                Tid = 1,
                Id = new Guid("6804C5AF-A163-4836-BDFA-8F0BC6295911"),
                Amount = 5000,
                Currency = "USD",
                CardHolderNumber = "336598********4587",
                HolderName = "Peter Obi",
                ExpirationMonth = 7,
                ExpirationYear = 29,
                CVV = 121,
                OrderReference = "REF-111112",
                Status = "Authorized",
                DateCreated = new DateTime(2023, 07, 10),
                DateUpdated = new DateTime(2023, 07, 10),

            };
            return new ServiceResponse<Payment>() { Data = response, ResponseMessage = string.Empty };            
        }
        public static ServiceResponse<Payment> GetSampleCapturedPaymentResponse_Repository()
        {
            var response = new Payment()
            {
                Tid = 2,
                Id = new Guid("D4D44D28-CFC7-496D-A072-3B46CBE8AA44"),
                Amount = 6500,
                Currency = "GBP",
                CardHolderNumber = "962548********3215",
                HolderName = "Chinargorom Osu",
                ExpirationMonth = 6,
                ExpirationYear = 25,
                CVV = 121,
                OrderReference = "REF-111113",
                Status = "Captured",
                DateCreated = new DateTime(2023, 08, 08),
                DateUpdated = new DateTime(2023, 08, 09),

            };
            return new ServiceResponse<Payment>() { Data = response, ResponseMessage = string.Empty };
        }
        public static ServiceResponse<Payment> GetSampleVoidedPaymentResponse_Repository()
        {
            var response = new Payment()
            {
                Tid = 3,
                Id = new Guid("513B6D20-C649-4AF5-AD3B-3FEE5704173C"),
                Amount = 1000,
                Currency = "USD",
                CardHolderNumber = "458651********2586",
                HolderName = "Peter Obi",
                ExpirationMonth = 7,
                ExpirationYear = 29,
                CVV = 121,
                OrderReference = "REF-111114",
                Status = "Voided",
                DateCreated = new DateTime(2023, 09, 11),
                DateUpdated = new DateTime(2023, 09, 12),

            };
            return new ServiceResponse<Payment>() { Data = response, ResponseMessage = string.Empty };
        }
    }
}
