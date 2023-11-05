using AcmePayService.API.Helpers;
using AcmePayService.Common.Static;
using AcmePayService.Domain.Command;
using AcmePayService.Domain.Command.Requests;
using AcmePayService.Domain.Queries;
using AcmePayService.Domain.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcmePayService.API.Controllers
{
    [Route("api/authorize")]
    [ApiController]
    public class PaymentProcessorController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// AuthorizationController constructor
        /// </summary>
        /// <param name="mediator"></param>
        public PaymentProcessorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// This endpoint method is for getting all transactions
        /// </summary>
        /// <param name="transactionRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTransactions([FromQuery] TransactionRequest transactionRequest)
        {   
            TransactionQuery transactionQuery = new(transactionRequest);
            var response = await _mediator.Send(transactionQuery);
            if (ResponseValidationHelper.IsBadRequest(response.ResponseMessage))
            {
                return BadRequest(response.ResponseMessage);
            }
            if (ResponseValidationHelper.IsInternalServerError(response.ResponseMessage))
            {
                return Problem(response.ResponseMessage);
            }
            return Ok(response.Data);
        }
        /// <summary>
        /// This endpoint method is for inserting authorized payment
        /// </summary>
        /// <param name="authorizeRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> AuthorizePayment([FromBody] AuthorizeRequest authorizeRequest)
        {
            if (!RequestValidationHelper.ValidateAuthourizePaymentRequest(authorizeRequest))
            {
                return BadRequest(ResponseMessages.InvalidInputKey);
            }
            AuthorizePaymentCommand paymentCommand = new(authorizeRequest);

            var response = await _mediator.Send(paymentCommand);
            if(ResponseValidationHelper.IsBadRequest(response.ResponseMessage)) 
            {
                return BadRequest(response.ResponseMessage);
            }
            if (ResponseValidationHelper.IsInternalServerError(response.ResponseMessage))
            {
                return Problem(response.ResponseMessage);
            }
            return Ok(response.Data);
        }
        /// <summary>
        /// This endpoint method is for capturing payments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="captureAndVoidInputRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{id}/capture")]
        public async Task<IActionResult>CapturePayment([FromRoute]Guid id,[FromBody] CaptureAndVoidInputRequest captureAndVoidInputRequest)
        {
            CaptureAndVoidCommandRequest request = new() { Id = id, OrderReference = captureAndVoidInputRequest.OrderReference };
            if (!RequestValidationHelper.ValidateCaptureOrVoidPaymentRequest(request))
            {
                return BadRequest(ResponseMessages.InvalidInputKey);
            }

            CapturePaymentCommand capturePaymentCommand = new (request);
            var response = await _mediator.Send(capturePaymentCommand);

            if (ResponseValidationHelper.IsBadRequest(response.ResponseMessage))
            {
                return BadRequest(response.ResponseMessage);
            }
            if (ResponseValidationHelper.IsInternalServerError(response.ResponseMessage))
            {
                return Problem(response.ResponseMessage);
            }
            return Ok(response.Data);
        }
        /// <summary>
        /// This endpoint method is for voiding payments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="captureAndVoidInputRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{id}/voids")]
        public async Task<IActionResult> VoidPayment([FromRoute] Guid id, [FromBody] CaptureAndVoidInputRequest captureAndVoidInputRequest)
        {
            CaptureAndVoidCommandRequest request = new() { Id = id, OrderReference = captureAndVoidInputRequest.OrderReference };
            if (!RequestValidationHelper.ValidateCaptureOrVoidPaymentRequest(request))
            {
                return BadRequest(ResponseMessages.InvalidInputKey);
            }

            VoidPaymentCommand voidPaymentCommand = new(request);
            var response = await _mediator.Send(voidPaymentCommand);

            if (ResponseValidationHelper.IsBadRequest(response.ResponseMessage))
            {
                return BadRequest(response.ResponseMessage);
            }
            if (ResponseValidationHelper.IsInternalServerError(response.ResponseMessage))
            {
                return Problem(response.ResponseMessage);
            }
            return Ok(response.Data);
        }
      
    }
}
