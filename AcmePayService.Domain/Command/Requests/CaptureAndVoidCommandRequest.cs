namespace AcmePayService.Domain.Command.Requests
{
    public class CaptureAndVoidCommandRequest
    {
        public Guid Id { get; set; }
        public string OrderReference { get; set; }
    }
}
