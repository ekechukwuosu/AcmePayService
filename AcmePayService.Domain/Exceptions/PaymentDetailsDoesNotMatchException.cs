using AcmePayService.Common.Static;
namespace AcmePayService.Domain.Exceptions
{
    public sealed class PaymentDetailsDoesNotMatchException : BadRequestException
    {
        public PaymentDetailsDoesNotMatchException() : base(ResponseMessages.PaymentNoMatchKey) { }
    } 
}
