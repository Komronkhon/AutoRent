using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Cars.DTOs;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Cars.GetCarById
{
    public sealed class GetCarByIdHandler : IRequestHandler<GetCarByIdQuery, _Result<CarResponse>>
    {
        private readonly ICarRepository _carRepository;

        public GetCarByIdHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<_Result<CarResponse>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByIdAsync(request.CarId, cancellationToken);

            if (car is null)
                return "Car not found.";

            return new CarResponse(
                car.Id,
                car.Brand,
                car.Model,
                car.Year,
                car.PricePerDay,
                car.Status,
                car.CreatedAt
            );
        }
    }
}
