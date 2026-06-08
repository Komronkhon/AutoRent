using CarRental.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Cars.DTOs
{
    public sealed record CarResponse(
        Guid Id,
        string Brand,
        string Model,
        int Year,
        decimal PricePerDay,
        CarStatus Status,
        DateTime CreatedAt
    );
}
