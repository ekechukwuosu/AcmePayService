using AcmePayService.Common.Static;
namespace AcmePayService.Domain.Exceptions
{
    public sealed class OrderReferenceExistsException : BadRequestException
    {
        public OrderReferenceExistsException() : base(ResponseMessages.OrderRefExistsKey)
        {
        }
    }
}
