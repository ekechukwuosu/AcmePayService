using AcmePayService.Common.Helper;
using AcmePayService.Domain.Queries.Responses;
using AcmePayService.Infrastructure.DAL.Repository.Interfaces;
using MediatR;

namespace AcmePayService.Domain.Queries.Handlers
{
    public class TransactionQueryHandler : IRequestHandler<TransactionQuery, ServiceResponse<TransactionResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;
        public TransactionQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        /// <summary>
        /// This is the handle method that performs the business logic for getting all transactions
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<TransactionResponse>> Handle(TransactionQuery request, CancellationToken cancellationToken)
        {
            var transactionRequest = request.TransactionRequest;
            var response = await _paymentRepository.GetPayments(transactionRequest.PageNumber, transactionRequest.PageSize);
            return new ServiceResponse<TransactionResponse>() { Data = new TransactionResponse(response.Data), ResponseMessage = response.ResponseMessage };
        }
    }
}
