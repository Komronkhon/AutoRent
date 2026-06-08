using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Cars.DTOs;
using CarRental.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Cars.GetCars
{
    public sealed class GetCarsHandler : IRequestHandler<GetCarsQuery, List<CarResponse>>
    {
        private readonly ICarRepository _carRepository;

        public GetCarsHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<CarResponse>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _carRepository.GetAllAsync(cancellationToken);

            return cars
                .Select(car => new CarResponse(
                    car.Id,
                    car.Brand,
                    car.Model,
                    car.Year,
                    car.PricePerDay,
                    car.Status,
                    car.CreatedAt))
                .ToList();
        }
    }
}
