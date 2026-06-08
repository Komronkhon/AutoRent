using CarRental.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Reservation.DTOs
{
    public sealed record ReservationResponse(
        Guid Id,
        Guid UserId,
        Guid CarId,
        DateTime StartDate,
        DateTime EndDate,
        ReservationStatus Status,
        DateTime CreatedAt
    );
}