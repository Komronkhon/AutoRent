using CarRental.Application.Features.Queries.Cars.DTOs;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Cars.GetCarById
{
    public sealed record GetCarByIdQuery(Guid CarId) : IRequest<_Result<CarResponse>>;
}
