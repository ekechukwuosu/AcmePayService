using AcmePayService.Domain.DTO;
using AcmePayService.Services.Command.Requests;
using MediatR;

namespace AcmePayService.Services.Command.CapturePayment
{
    public record CapturePaymentCommand(CaptureAndVoidCommandRequest CaptureAndVoidRequest) : IRequest<PaymentDTO>;
}
