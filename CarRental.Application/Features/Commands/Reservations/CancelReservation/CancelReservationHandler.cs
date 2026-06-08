using CarRental.Application.Abstractions;
using CarRental.Domain.Common;
using CarRental.Domain.Enums;
using MediatR;

namespace CarRental.Application.Features.Commands.Reservations.CancelReservation
{
    public class CancelReservationHandler : IRequestHandler<CancelReservationCommand, _Result>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelReservationHandler(
            IReservationRepository reservationRepository, 
            IUnitOfWork unitOfWork,
            ICarRepository carRepository)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
        }

        public async Task<_Result> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
                return "Reservation not found.";

            var car = await _carRepository.GetByIdAsync(reservation.CarId, cancellationToken);

            if (car is null)
                return "Car not found.";

            var result = reservation.Cancel();

            if (result.IsFailure)
                return result.Error!;

            car.MakeAvailable();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
