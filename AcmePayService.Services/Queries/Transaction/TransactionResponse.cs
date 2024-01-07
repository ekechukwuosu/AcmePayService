using AcmePayService.Common;
using AcmePayService.Domain.Models;

namespace AcmePayService.Services.Queries.Transaction
{
    public record TransactionResponse(PagedList<PaymentReportDTO> PaymentReport);
}
