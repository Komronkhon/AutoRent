using CarRental.Application.Features.Queries.Cars.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Cars.GetCars
{
    public sealed record GetCarsQuery : IRequest<List<CarResponse>>;
}
