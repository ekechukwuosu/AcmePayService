using AcmePayService.Domain.DTO;
using AcmePayService.Domain.Entities;
using AcmePayService.Domain.Enums;
using AcmePayService.Domain.Exceptions;
using AcmePayService.Domain.RepositoryInterfaces;
using AutoMapper;
using MediatR;

namespace AcmePayService.Services.Command.AuthorizePayment
{
    public class AuthorizePaymentRequestHandler : IRequestHandler<AuthorizePaymentCommand, PaymentDTO>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public AuthorizePaymentRequestHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// This is the handle method that performs the business logic for the authorize payment
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaymentDTO> Handle(AuthorizePaymentCommand request, CancellationToken cancellationToken)
        {
            var paymentRequest = request.PaymentRequest;
            var payment = _mapper.Map<AuthorizeRequest, Payment>(paymentRequest);
            bool ifExists = await _paymentRepository.CheckIfPaymentExists(payment.OrderReference);
            if (ifExists)
            {
                throw new OrderReferenceExistsException();
            }
            var response = await _paymentRepository.AddPayment(PaymentStatus.Authorized, payment);
            return _mapper.Map<Payment, PaymentDTO>(response);
        }
    }
}



