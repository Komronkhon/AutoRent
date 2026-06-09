using CarRental.Application.Abstractions;
using CarRental.Application.Features.Commands.Users.RegisterUser;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Reservations.DeleteReservation
{
    public sealed class DeleteReservationHandler : IRequestHandler<DeleteReservationCommand, _Result>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReservationHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByGuidAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
                return "User not found";

            reservation.Delete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
