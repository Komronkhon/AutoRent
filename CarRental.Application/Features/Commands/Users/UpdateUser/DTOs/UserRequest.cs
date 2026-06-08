using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Users.UpdateUser.DTOs
{
    public sealed record UserRequest(
        Guid UserId,
        string FullName,
        string Email,
        string PhoneNumber,
        string PassportNumber
    );
}
