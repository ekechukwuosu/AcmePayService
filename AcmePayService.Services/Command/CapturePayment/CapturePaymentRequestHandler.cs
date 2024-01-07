using AcmePayService.Domain.DTO;
using AcmePayService.Domain.Entities;
using AcmePayService.Domain.Enums;
using AcmePayService.Domain.RepositoryInterfaces;
using AcmePayService.Services.Command.Requests;
using AutoMapper;
using MediatR;

namespace AcmePayService.Services.Command.CapturePayment
{
    public class CapturePaymentRequestHandler : IRequestHandler<CapturePaymentCommand, PaymentDTO>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public CapturePaymentRequestHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// This is the handle method that performs the business logic for the capture payment
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaymentDTO> Handle(CapturePaymentCommand request, CancellationToken cancellationToken)
        {
            var captureAndVoidRequest = request.CaptureAndVoidRequest;
            var payment = _mapper.Map<CaptureAndVoidCommandRequest, Payment>(captureAndVoidRequest);
            var response = await _paymentRepository.UpdatePayment(PaymentStatus.Captured, payment);
            return _mapper.Map<Payment, PaymentDTO>(response);
        }
    }
}
