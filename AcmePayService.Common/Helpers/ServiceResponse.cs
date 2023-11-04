namespace AcmePayService.Common.Helper
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string ResponseMessage { get; set; }
    }
}
