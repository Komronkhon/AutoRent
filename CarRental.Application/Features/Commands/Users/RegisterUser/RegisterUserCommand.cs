using CarRental.Application.Features.Queries.Users.DTOs;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Users.RegisterUser
{
    public sealed record RegisterUserCommand(
        string FullName,
        string Email,
        string PhoneNumber,
        string PassportNumber
    ) : IRequest<_Result<UserResponse>>;
}
