using MediatR;

namespace AcmePayService.Services.Queries.Transaction
{
    public record TransactionQuery(TransactionRequest TransactionRequest) : IRequest<TransactionResponse>;
}
