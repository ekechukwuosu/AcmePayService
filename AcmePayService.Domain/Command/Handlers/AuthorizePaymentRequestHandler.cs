using AcmePayService.Common.Helper;
using AcmePayService.Domain.Command.Requests;
using AcmePayService.Domain.Command.Responses;
using AcmePayService.Infrastructure.DAL.Repository.Interfaces;
using AcmePayService.Infrastructure.Data.Enums;
using AcmePayService.Infrastructure.Data.Models;
using AutoMapper;
using MediatR;

namespace AcmePayService.Domain.Command.Handlers
{
    public class AuthorizePaymentRequestHandler : IRequestHandler<AuthorizePaymentCommand, ServiceResponse<PaymentResponse>>
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
        public async Task<ServiceResponse<PaymentResponse>> Handle(AuthorizePaymentCommand request, CancellationToken cancellationToken)
        {
            var paymentRequest = request.PaymentRequest;
            var payment = _mapper.Map<AuthorizeRequest, Payment>(paymentRequest);
            var response = await _paymentRepository.ActionPayment(PaymentStatus.Authorized, payment);
            return new ServiceResponse<PaymentResponse>() {Data = new PaymentResponse(response.Data.Id, response.Data.Status), ResponseMessage = response.ResponseMessage };
            
        } 
    }
}



