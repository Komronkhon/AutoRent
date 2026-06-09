using CarRental.Application.Features.Queries.Cars.DTOs;
using CarRental.Domain.Common;
using CarRental.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Cars.UpdateCar
{
    public sealed record UpdateCarCommand(
        Guid CarId,
        string Brand,
        string Model,
        int Year,
        decimal PricePerDay)
        : IRequest<_Result<CarResponse>>;
}