using AcmePayService.Domain.RepositoryInterfaces;
using MediatR;

namespace AcmePayService.Services.Queries.Transaction
{
    public class TransactionQueryHandler : IRequestHandler<TransactionQuery, TransactionResponse>
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
        public async Task<TransactionResponse> Handle(TransactionQuery request, CancellationToken cancellationToken)
        {
            var transactionRequest = request.TransactionRequest;
            var response = await _paymentRepository.GetPayments(transactionRequest.PageNumber, transactionRequest.PageSize);
            return new TransactionResponse(response);
        }
    }
}
