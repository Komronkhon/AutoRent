using CarRental.Application.Abstractions;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Common;
using CarRental.Domain.ValueObjects;
using MediatR;

namespace CarRental.Application.Features.Commands.Reservations.CreateReservation
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, _Result<Guid>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReservationHandler(
            IReservationRepository reservationRepository, 
            IUnitOfWork unitOfWork,
            ICarRepository carRepository,
            IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
            _userRepository = userRepository;
        }

        public async Task<_Result<Guid>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByGuidAsync(request.UserId, cancellationToken);

            if (user is null)
                return "User not found.";

            var car = await _carRepository.GetByGuidAsync(request.CarId, cancellationToken);

            if (car is null)
                return "Car not found.";

            var rentalPeriod = RentalPeriod.Create(request.StartDate, request.EndDate);

            if (rentalPeriod.IsFailure)
                return rentalPeriod.Error!;

            var hasConflict = await _reservationRepository.HasOverlappingReservationAsync(
                request.CarId, 
                request.StartDate, 
                request.EndDate, 
                cancellationToken);

            if(hasConflict)
                return "Car is already reserved.";

            var reservation = Reservation.Create(request.UserId, request.CarId, rentalPeriod.Value);

            if (reservation.IsFailure)
                return reservation.Error!;

            var reserveResult = car.Reserve();

            if (reserveResult.IsFailure)
                return reserveResult.Error!;

            await _reservationRepository.AddAsync(reservation.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reservation.Value.Id;
        }
    }
}
