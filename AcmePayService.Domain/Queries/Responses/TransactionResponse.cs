using AcmePayService.Infrastructure.Data.Models;

namespace AcmePayService.Domain.Queries.Responses
{
    public record TransactionResponse(List<PaymentReport> PaymentReport);
}
