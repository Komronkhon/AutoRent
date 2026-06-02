using CarRental.Application.Abstractions;
using CarRental.Domain.Common;
using CarRental.Domain.Enums;
using MediatR;

namespace CarRental.Application.Features.Reservations.CancelReservation
{
    public class CancelReservationHandler : IRequestHandler<CancelReservationCommand, _Result>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelReservationHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
                return "Reservation not found.";

            var result = reservation.Cancel();

            if (result.IsFailure)
                return result.Error!;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
