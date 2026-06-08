using CarRental.Application.Abstractions;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Reservations.ConfirmReservation
{
    public sealed class ConfirmReservationHandler : IRequestHandler< ConfirmReservationCommand, _Result>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmReservationHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result> Handle(ConfirmReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation =  await _reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
                return "Reservation not found.";

            var result = reservation.Confirm();

            if (result.IsFailure)
                return result.Error!;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}