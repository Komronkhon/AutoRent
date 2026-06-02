using CarRental.Application.Abstractions;
using CarRental.Application.Features.Reservations.ConfirmReservation;
using CarRental.Domain.Common;
using CarRental.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Reservations.CompleteReservation
{
    public class CompleteReservationHandler : IRequestHandler<CompleteReservationCommand, _Result>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteReservationHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result> Handle(CompleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
                return "Reservation not found.";

            if (reservation.Status != ReservationStatus.Confirmed)
                return "Only confirmed reservations can be completed.";

            var result = reservation.Complete();

            if(result.IsFailure)
                return result.Error!;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
