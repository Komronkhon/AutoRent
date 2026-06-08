using CarRental.Application.Features.Queries.Users.DTOs;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Users.GetUserById
{
    public sealed record GetUserByIdQuery(Guid UserId)
        : IRequest<_Result<UserResponse>>;
}
