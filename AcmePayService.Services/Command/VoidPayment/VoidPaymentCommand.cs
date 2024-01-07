using AcmePayService.Domain.DTO;
using AcmePayService.Services.Command.Requests;
using MediatR;

namespace AcmePayService.Services.Command.VoidPayment
{
    public record VoidPaymentCommand(CaptureAndVoidCommandRequest CaptureAndVoidRequest) : IRequest<PaymentDTO>;
}
