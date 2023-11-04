namespace AcmePayService.Common.Static
{
    public class ResponseMessages
    {
        public const string InvalidInputKey = "Input values are invalid.";
        public const string PaymentNoMatchKey = "The payment details do not match any transaction in the system. Kindly check and try again";
        public const string OrderRefExistsKey = "A payment with this order reference already exists";
        public const string SystemFailureKey = "The request is failed because of a system failure";
    }
}
