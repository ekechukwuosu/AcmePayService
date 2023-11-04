using AcmePayService.Common.Helper;
using AcmePayService.Infrastructure.Data.Enums;
using AcmePayService.Infrastructure.Data.Models;

namespace AcmePayService.Infrastructure.DAL.Repository.Interfaces
{
    public interface IPaymentRepository
    {
        Task<ServiceResponse<List<PaymentReport>>> GetPayments(int pageNumber, int pageSize);
        Task<ServiceResponse<Payment>> ActionPayment(PaymentStatus paymentStatus, Payment payment);

    }
}
