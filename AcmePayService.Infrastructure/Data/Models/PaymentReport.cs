namespace AcmePayService.Infrastructure.Data.Models
{
    public record PaymentReport
    {
        public PaymentReport(decimal amount, string currency, string cardHolderNumber, string holderName, Guid id, string status)
        {
            Amount = amount;
            Currency = currency;
            CardHolderNumber = cardHolderNumber;
            HolderName = holderName;
            Id = id;
            Status = status;
        }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardHolderNumber { get; set; }
        public string HolderName { get; set; }
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
