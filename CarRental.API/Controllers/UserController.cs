using CarRental.Application.Features.Commands.Users.DeleteUser;
using CarRental.Application.Features.Commands.Users.RegisterUser;
using CarRental.Application.Features.Commands.Users.UpdateUser;
using CarRental.Application.Features.Commands.Users.UpdateUser.DTOs;
using CarRental.Application.Features.Queries.Users;
using CarRental.Application.Features.Queries.Users.GetUserById;
using CarRental.Application.Features.Queries.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetUsersQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetUserByIdQuery(id), cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UserRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateUserCommand(
                    id,
                    request.FullName,
                    request.Email,
                    request.PhoneNumber,
                    request.PassportNumber),
                    cancellationToken
                );

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new DeleteUserCommand(id), cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return NoContent();
        }
    }
}
