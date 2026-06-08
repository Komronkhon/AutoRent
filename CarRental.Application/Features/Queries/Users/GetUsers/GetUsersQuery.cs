using CarRental.Application.Features.Queries.Users.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Users.GetUsers
{
    public sealed record GetUsersQuery : IRequest<List<UserResponse>>;
}
