using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Cars.DTOs;
using CarRental.Domain.Common;
using MediatR;

namespace CarRental.Application.Features.Commands.Cars.UpdateCar
{
    public sealed class UpdateCarHandler : IRequestHandler<UpdateCarCommand, _Result<CarResponse>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCarHandler(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result<CarResponse>> Handle(UpdateCarCommand command, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByGuidAsync(command.CarId, cancellationToken);

            if (car is null)
                return "Car not found";

            var result = car!.Update(
                command.Brand,
                command.Model,
                command.Year,
                command.PricePerDay
            );

            if (result.IsFailure)
                return result.Error!;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

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
