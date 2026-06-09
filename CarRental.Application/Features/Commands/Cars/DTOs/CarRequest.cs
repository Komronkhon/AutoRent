using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Cars.DTOs
{
    public sealed record CarRequest(
        string Brand,
        string Model,
        int Year,
        decimal PricePerDay
    );
}
