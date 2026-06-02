using CarRental.Application.Abstractions;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CarRental.Application.Features.Cars.CreateCar
{
    public class CreateCarHandler : IRequestHandler<CreateCarCommand, _Result<Guid>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCarHandler(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result<Guid>> Handle(CreateCarCommand command, CancellationToken cancellationToken)
        {
            var carResult = Car.Create(
                command.Brand,
                command.Model,
                command.Year,
                command.PricePerDay);

            if (carResult.IsFailure)
                return carResult.Error!;

            await _carRepository.AddAsync(carResult.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return carResult.Value.Id;
        }
    }
}
