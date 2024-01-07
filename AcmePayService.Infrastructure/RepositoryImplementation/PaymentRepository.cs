using AcmePayService.Common;
using AcmePayService.Common.Helper;
using AcmePayService.Domain.Entities;
using AcmePayService.Domain.Enums;
using AcmePayService.Domain.Exceptions;
using AcmePayService.Domain.Models;
using AcmePayService.Domain.RepositoryInterfaces;
using AcmePayService.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcmePayService.Infrastructure.RepositoryImplementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDBContext _dB;
        private readonly ILogger<PaymentRepository> _logger;
        public PaymentRepository(AppDBContext dB, ILogger<PaymentRepository> logger)
        {
            _dB = dB;
            _logger = logger;
        }
        /// <summary>
        /// This is the repository method that gets all payments
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<PaymentReportDTO>> GetPayments(int pageNumber, int pageSize)
        {
            List<PaymentReportDTO> payments = new List<PaymentReportDTO>();
                payments = await _dB.Payments
                            .Select(b => new PaymentReportDTO(b.Amount, b.Currency, b.CardHolderNumber, b.HolderName, b.Id, b.Status))
                            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();            

            return PagedList<PaymentReportDTO>.ToPagedList(payments, pageNumber, pageSize);
        }
        /// <summary>
        /// This is a repository method for authorizing a payment
        /// </summary>
        /// <param name="paymentStatus"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<Payment> AddPayment(PaymentStatus paymentStatus, Payment payment)
        {
            payment.Id = Guid.NewGuid();
            payment.Status = EnumHelper.GetEnumDescription(paymentStatus);
            await _dB.Payments.AddAsync(payment);
            _dB.SaveChanges();              
           
            return payment;
        }
        /// <summary>
        /// This is a repository method for capturing or voiding a payment
        /// </summary>
        /// <param name="paymentStatus"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<Payment> UpdatePayment(PaymentStatus paymentStatus, Payment payment)
        {
            //Update Payment with new status;
                payment = await _dB.Payments.Where(a => a.Id == payment.Id && a.OrderReference == payment.OrderReference).FirstOrDefaultAsync();
                if (payment != null)
                {
                    payment.Status = EnumHelper.GetEnumDescription(paymentStatus);
                    payment.DateUpdated = DateTime.Now;
                    _dB.Payments.Update(payment);
                    _dB.SaveChanges();
                }
                else
                {
                    throw new PaymentDetailsDoesNotMatchException(); 
                }
           
            return payment;
        }
        /// <summary>
        /// This is a repository method for checking if a payment exists
        /// </summary>
        /// <param name="orderReference"></param>
        /// <returns></returns>

        public async Task<bool> CheckIfPaymentExists(string orderReference)
        {
            return await _dB.Payments
                .AnyAsync(a  => a.OrderReference == orderReference);
        }
    }    
}
