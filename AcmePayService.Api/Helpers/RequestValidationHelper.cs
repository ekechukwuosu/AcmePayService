using AcmePayService.Domain.Command.Requests;
using AcmePayService.Domain.Queries.Requests;
using System.Transactions;

namespace AcmePayService.API.Helpers
{
    /// <summary>
    /// This class contails methods for validating controller method input requests
    /// </summary>
    public class RequestValidationHelper
    {
        /// <summary>
        /// This helper method validates transaction request for Getting all transactions
        /// </summary>
        /// <param name="transactionRequest"></param>
        /// <returns></returns>
        public static bool ValidateTransactionRequest(TransactionRequest transactionRequest)
        {
            if (transactionRequest == null || transactionRequest.PageNumber == 0  || transactionRequest.PageSize == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// This helper method validates input request for Authorize payment
        /// </summary>
        /// <param name="authorizeRequest"></param>
        /// <returns></returns>
        public static bool ValidateAuthourizePaymentRequest(AuthorizeRequest authorizeRequest)
        {
            if (authorizeRequest == null || authorizeRequest.Amount <= 0 || string.IsNullOrEmpty(authorizeRequest.Currency) || 
                string.IsNullOrEmpty(authorizeRequest.CardHolderNumber) || string.IsNullOrEmpty(authorizeRequest.HolderName) ||
                authorizeRequest.ExpirationMonth <= 0 || authorizeRequest.ExpirationYear <= 0 || authorizeRequest.CVV <= 0 || authorizeRequest.CVV.ToString().Length != 3 ||
                string.IsNullOrEmpty(authorizeRequest.OrderReference))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// This helper method validates input request for Capture payment and Void payment
        /// </summary>
        /// <param name="captureAndVoidCommandRequest"></param>
        /// <returns></returns>
        public static bool ValidateCaptureOrVoidPaymentRequest(CaptureAndVoidCommandRequest captureAndVoidCommandRequest )
        {
            if (captureAndVoidCommandRequest == null || !IsGuid(captureAndVoidCommandRequest.Id.ToString()) || string.IsNullOrEmpty(captureAndVoidCommandRequest.OrderReference))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// This helper method validates if a string is a valid guid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }
    }
}
