using CarRental.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Payments.DTOs
{
    public sealed record PaymentResponse(
        Guid Id,
        Guid ReservationId,
        decimal Amount,
        PaymentStatus Status,
        PaymentType Type,
        DateTime CreatedAt
    );
}
    