using CarRental.Application.Abstractions;
using CarRental.Application.Features.Commands.Users.RegisterUser;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Cars.DeleteCar
{
    public sealed class DeleteCarHandler : IRequestHandler<DeleteReservationCommand, _Result>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCarHandler(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByGuidAsync(request.CarId, cancellationToken);

            if (car is null)
                return "User not found";

            car.Delete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
