using AcmePayService.Domain.DTO;
using AcmePayService.Domain.Entities;
using AcmePayService.Domain.Enums;
using AcmePayService.Domain.RepositoryInterfaces;
using AcmePayService.Services.Command.Requests;
using AutoMapper;
using MediatR;

namespace AcmePayService.Services.Command.VoidPayment
{
    public class VoidPaymentRequestHandler : IRequestHandler<VoidPaymentCommand, PaymentDTO>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public VoidPaymentRequestHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// This is the handle method that performs the business logic for the void payment
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaymentDTO> Handle(VoidPaymentCommand request, CancellationToken cancellationToken)
        {
            var captureAndVoidRequest = request.CaptureAndVoidRequest;
            var payment = _mapper.Map<CaptureAndVoidCommandRequest, Payment>(captureAndVoidRequest);
            var response = await _paymentRepository.UpdatePayment(PaymentStatus.Voided, payment);
            return _mapper.Map<Payment, PaymentDTO>(response);
        }
    }
}
