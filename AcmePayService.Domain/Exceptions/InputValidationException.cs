using AcmePayService.Common.Static;

namespace AcmePayService.Domain.Exceptions
{
    public class InputValidationException : BadRequestException
    {
        public InputValidationException() : base(ResponseMessages.InvalidInputKey)
        {
        }
    }
}
