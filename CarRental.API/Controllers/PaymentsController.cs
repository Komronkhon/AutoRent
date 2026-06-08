using CarRental.Application.Features.Commands.Payments.ConfirmPayment;
using CarRental.Application.Features.Commands.Payments.CreatePayment;
using CarRental.Application.Features.Commands.Payments.FailPayment;
using CarRental.Application.Features.Commands.Payments.RefundPayment;
using CarRental.Application.Features.Queries.Payments.GetPayment;
using CarRental.Application.Features.Queries.Payments.GetReservationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class PaymentsController : ControllerBase
    {
        private readonly ISender _sender;

        public PaymentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetPaymentsQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetPaymentByIdQuery(id), cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/confirm")]
        public async Task<IActionResult> Confirm(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ConfirmPaymentCommand(id), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPut("{id:guid}/fail")]
        public async Task<IActionResult> Fail(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new FailPaymentCommand(id), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPut("{id:guid}/refund")]
        public async Task<IActionResult> Refund(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new RefundPaymentCommand(id), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
