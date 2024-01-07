using AcmePayService.Domain.DTO;
using MediatR;

namespace AcmePayService.Services.Command.AuthorizePayment
{
    public record AuthorizePaymentCommand(AuthorizeRequest PaymentRequest) : IRequest<PaymentDTO>;
}
