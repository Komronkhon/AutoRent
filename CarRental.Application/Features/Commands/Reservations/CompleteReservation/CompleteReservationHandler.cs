using CarRental.Application.Abstractions;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Common;
using CarRental.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Reservations.CompleteReservation
{
    public class CompleteReservationHandler : IRequestHandler<CompleteReservationCommand, _Result>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteReservationHandler(
            IReservationRepository reservationRepository, 
            IUnitOfWork unitOfWork,
            ICarRepository carRepository)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
        }

        public async Task<_Result> Handle(CompleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByGuidAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
                return "Reservation not found.";

            if (reservation.Status != ReservationStatus.Confirmed)
                return "Only confirmed reservations can be completed.";

            var car = await _carRepository.GetByGuidAsync(reservation.CarId, cancellationToken);

            if (car is null)
                return "Car not found.";

            var result = reservation.Complete();

            if(result.IsFailure)
                return result.Error!;

            car.MakeAvailable();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
