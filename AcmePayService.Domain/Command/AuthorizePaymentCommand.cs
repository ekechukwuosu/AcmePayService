using AcmePayService.Common.Helper;
using AcmePayService.Domain.Command.Requests;
using AcmePayService.Domain.Command.Responses;
using MediatR;

namespace AcmePayService.Domain.Command
{
    public record AuthorizePaymentCommand(AuthorizeRequest PaymentRequest) : IRequest<ServiceResponse<PaymentResponse>>;
}
