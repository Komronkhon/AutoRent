using CarRental.Application.Features.Commands.Cars.CreateCar;
using CarRental.Application.Features.Commands.Cars.DeleteCar;
using CarRental.Application.Features.Commands.Cars.DTOs;
using CarRental.Application.Features.Commands.Cars.UpdateCar;
using CarRental.Application.Features.Commands.Users.DTOs;
using CarRental.Application.Features.Queries.Cars.GetCarById;
using CarRental.Application.Features.Queries.Cars.GetCars;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class CarsController : ControllerBase
    {
        private readonly ISender _sender;

        public CarsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetCarsQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetCarByIdQuery(id), cancellationToken);
            
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, CarRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateCarCommand(
                    id,
                    request.Brand,
                    request.Model,
                    request.Year,
                    request.PricePerDay),
                    cancellationToken
                );

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new DeleteReservationCommand(id), cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return NoContent();
        }
    }
}
