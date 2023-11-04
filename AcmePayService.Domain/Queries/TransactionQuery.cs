using AcmePayService.Common.Helper;
using AcmePayService.Domain.Queries.Requests;
using AcmePayService.Domain.Queries.Responses;
using MediatR;

namespace AcmePayService.Domain.Queries
{
    public record TransactionQuery(TransactionRequest TransactionRequest) : IRequest<ServiceResponse<TransactionResponse>>;
}
