using AcmePayService.Common.Static;

namespace AcmePayService.API.Helpers
{/// <summary>
/// This class validates response messages
/// </summary>
    public class ResponseValidationHelper
    {
        /// <summary>
        /// Validate if errormessage matches bad request
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool IsBadRequest(string errorMessage)
        {
            return errorMessage == ResponseMessages.PaymentNoMatchKey || errorMessage == ResponseMessages.OrderRefExistsKey;
        }
        /// <summary>
        /// Validate if errormessage mathches InternalServerError
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool IsInternalServerError(string errorMessage)
        {
            return errorMessage == ResponseMessages.SystemFailureKey;
        }
    }
}
