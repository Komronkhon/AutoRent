using CarRental.Application.Features.Commands.Reservations.CancelReservation;
using CarRental.Application.Features.Commands.Reservations.CompleteReservation;
using CarRental.Application.Features.Commands.Reservations.ConfirmReservation;
using CarRental.Application.Features.Commands.Reservations.CreateReservation;
using CarRental.Application.Features.Queries.Reservation.GetReservationById;
using CarRental.Application.Features.Queries.Reservation.GetReservations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ReservationsController : ControllerBase
    {
        private readonly ISender _sender;

        public ReservationsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetReservationsQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetReservationByIdQuery(id), cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/confirm")]
        public async Task<IActionResult> Confirm(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ConfirmReservationCommand(id), cancellationToken);
            
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new CancelReservationCommand(id), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPut("{id:guid}/complete")]
        public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new CompleteReservationCommand(id), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
