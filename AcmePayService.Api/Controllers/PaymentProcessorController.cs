using AcmePayService.API.Helpers;
using AcmePayService.Domain.Exceptions;
using AcmePayService.Services.Command.AuthorizePayment;
using AcmePayService.Services.Command.CapturePayment;
using AcmePayService.Services.Command.Requests;
using AcmePayService.Services.Command.VoidPayment;
using AcmePayService.Services.Queries.Transaction;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTransactions([FromQuery] TransactionRequest transactionRequest)
        {   
            TransactionQuery transactionQuery = new(transactionRequest);
            var response = await _mediator.Send(transactionQuery);
            return Ok(response);
        }
        /// <summary>
        /// This endpoint method is for inserting authorized payment
        /// </summary>
        /// <param name="authorizeRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthorizePayment([FromBody] AuthorizeRequest authorizeRequest)
        {
            if (!RequestValidationHelper.ValidateAuthourizePaymentRequest(authorizeRequest))
            {
                throw new InputValidationException();
            }
            AuthorizePaymentCommand paymentCommand = new(authorizeRequest);

            var response = await _mediator.Send(paymentCommand);
            
            return Ok(response);
        }
        /// <summary>
        /// This endpoint method is for capturing payments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="captureAndVoidInputRequest"></param>
        /// <returns></returns>
        [HttpPost("{id}/capture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>CapturePayment([FromRoute]Guid id,[FromBody] CaptureAndVoidInputRequest captureAndVoidInputRequest)
        {
            CaptureAndVoidCommandRequest request = new() { Id = id, OrderReference = captureAndVoidInputRequest.OrderReference };
            if (!RequestValidationHelper.ValidateCaptureOrVoidPaymentRequest(request))
            {
                throw new InputValidationException();
            }

            CapturePaymentCommand capturePaymentCommand = new (request);
            var response = await _mediator.Send(capturePaymentCommand);
            return Ok(response);
        }
        /// <summary>
        /// This endpoint method is for voiding payments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="captureAndVoidInputRequest"></param>
        /// <returns></returns>
        [HttpPost("{id}/voids")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VoidPayment([FromRoute] Guid id, [FromBody] CaptureAndVoidInputRequest captureAndVoidInputRequest)
        {
            CaptureAndVoidCommandRequest request = new() { Id = id, OrderReference = captureAndVoidInputRequest.OrderReference };
            if (!RequestValidationHelper.ValidateCaptureOrVoidPaymentRequest(request))
            {
                throw new InputValidationException();
            }

            VoidPaymentCommand voidPaymentCommand = new(request);
            var response = await _mediator.Send(voidPaymentCommand);
            return Ok(response);
        }
      
    }
}
