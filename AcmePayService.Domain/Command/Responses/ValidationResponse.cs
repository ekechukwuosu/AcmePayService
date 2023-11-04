namespace AcmePayService.Domain.Command.Responses
{
    public class ValidationResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
