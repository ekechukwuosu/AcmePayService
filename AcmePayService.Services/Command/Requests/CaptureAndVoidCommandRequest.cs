namespace AcmePayService.Services.Command.Requests
{
    public class CaptureAndVoidCommandRequest
    {
        public Guid Id { get; set; }
        public string OrderReference { get; set; }
    }
}
