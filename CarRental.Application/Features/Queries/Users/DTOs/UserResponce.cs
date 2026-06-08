using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Users.DTOs
{
    public sealed record UserResponse(
        Guid Id,
        string FullName,
        string Email,
        string PhoneNumber,
        string PassportNumber,
        DateTime CreatedAt
    );
}
