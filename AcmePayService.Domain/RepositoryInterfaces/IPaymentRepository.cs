using AcmePayService.Common;
using AcmePayService.Domain.Entities;
using AcmePayService.Domain.Enums;
using AcmePayService.Domain.Models;

namespace AcmePayService.Domain.RepositoryInterfaces
{
    public interface IPaymentRepository
    {
        Task<PagedList<PaymentReportDTO>> GetPayments(int pageNumber, int pageSize);
        Task<bool> CheckIfPaymentExists(string orderReference);
        Task<Payment> AddPayment(PaymentStatus paymentStatus, Payment payment);
        Task<Payment> UpdatePayment(PaymentStatus paymentStatus, Payment payment);

    }
}
