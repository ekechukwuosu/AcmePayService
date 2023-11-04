using AcmePayService.Common.Helper;
using AcmePayService.Common.Static;
using AcmePayService.Infrastructure.DAL.Repository.Interfaces;
using AcmePayService.Infrastructure.Data.DB;
using AcmePayService.Infrastructure.Data.Enums;
using AcmePayService.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcmePayService.Infrastructure.DAL.Repository.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDBContext _dB;
        private readonly ILogger<PaymentRepository> _logger;
        private static string errorMessage;
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
        public async Task<ServiceResponse<List<PaymentReport>>> GetPayments(int pageNumber, int pageSize)
        {
            errorMessage = string.Empty;
            List<PaymentReport> payments = new List<PaymentReport>();
            try
            {
                payments = await _dB.Payments
                    .Select(b => new PaymentReport(b.Amount, b.Currency, b.CardHolderNumber, b.HolderName, b.Id, b.Status))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                errorMessage = $"{ResponseMessages.SystemFailureKey} - {ex.Message}";
                _logger.LogError($"Error in PaymentRepository class, GetPayments Method. Message : {ex.Message}");
            }
            return new ServiceResponse<List<PaymentReport>>() { Data = payments, ResponseMessage = errorMessage };
        }
        /// <summary>
        /// This is a repository method for authrizing, capturing and voiding payment
        /// </summary>
        /// <param name="paymentStatus"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Payment>> ActionPayment(PaymentStatus paymentStatus, Payment payment)
        {
            errorMessage = string.Empty;
            try
            {
                if (paymentStatus == PaymentStatus.Authorized)
                {
                    //Persisiting payment request
                    var paymentExists = await _dB.Payments.Where(a => a.OrderReference == payment.OrderReference).FirstOrDefaultAsync();
                    if(paymentExists == null)
                    {
                        payment.Id = Guid.NewGuid();
                        payment.Status = EnumHelper.GetEnumDescription(paymentStatus);
                        await _dB.Payments.AddAsync(payment);
                    }
                    else
                    {
                        errorMessage = ResponseMessages.OrderRefExistsKey;
                    }
                }
                else
                {
                    //Update Payment with new status;
                    payment = await _dB.Payments.Where(a => a.Id == payment.Id && a.OrderReference == payment.OrderReference).FirstOrDefaultAsync();
                    if (payment != null)
                    {
                        payment.Status = EnumHelper.GetEnumDescription(paymentStatus);
                        payment.DateUpdated = DateTime.Now;
                        _dB.Payments.Update(payment);
                    }
                    else
                    {
                        errorMessage = ResponseMessages.PaymentNoMatchKey;
                    }
                }
                _dB.SaveChanges();
            }
            catch (Exception ex)
            {
                errorMessage = $"{ResponseMessages.SystemFailureKey} - {ex.Message}";
                _logger.LogError($"Error in PaymentRepository class, ActionPayment Method. Message : {ex.Message}");
            }
            return new ServiceResponse<Payment>(){ Data = payment == null? new Payment() : payment, ResponseMessage = errorMessage};
        }
    
    }
}
