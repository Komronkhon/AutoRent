using CarRental.Application.Abstractions;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Common;
using CarRental.Domain.ValueObjects;
using MediatR;

namespace CarRental.Application.Features.Reservations.CreateReservation
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, _Result<Guid>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReservationHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result<Guid>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var rentalPeriod = RentalPeriod.Create(request.StartDate, request.EndDate);

            var hasConflict = await _reservationRepository.HasOverlappingReservationAsync(
                request.CarId, 
                request.StartDate, 
                request.EndDate, 
                cancellationToken);

            if(hasConflict)
                return "Car is already reserved.";

            var reservation = Reservation.Create(request.UserId, request.CarId, rentalPeriod.Value);

            await _reservationRepository.AddAsync(reservation.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reservation.Value.Id;
        }
    }
}
