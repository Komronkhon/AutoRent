using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Users.DeleteUser
{
    public sealed record DeleteUserCommand(Guid UserId) : IRequest<_Result>;
}
