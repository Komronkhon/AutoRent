using CarRental.Application.Abstractions;
using CarRental.Application.Features.Commands.Payments.FailPayment;
using CarRental.Domain.Common;
using CarRental.Domain.Enums;
using MediatR;

namespace CarRental.Application.Features.Commands.Payments.ConfirmPayment
{
    public class ConfirmPaymentHandler : IRequestHandler<ConfirmPaymentCommand, _Result>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmPaymentHandler(
            IPaymentRepository paymentRepository, 
            IUnitOfWork unitOfWork,
            IReservationRepository reservationRepository,
            ICarRepository carRepository
            )
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
            _reservationRepository = reservationRepository;
            _carRepository = carRepository;
        }

        public async Task<_Result> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByGuidAsync(request.PaymentId, cancellationToken);

            if (payment is null)
                return "Payment not found.";

            var result = payment.Confirm();

            if (result.IsFailure)
                return result.Error!;

            var reservation = await _reservationRepository.GetByGuidAsync(payment.ReservationId, cancellationToken);

            if (reservation is null)
                return "Reservation not found.";

            var reservationResult = reservation.Confirm();

            if (reservationResult.IsFailure)
                return reservationResult.Error!;

            var car = await _carRepository.GetByGuidAsync(reservation.CarId, cancellationToken);

            if (car is null)
                return "Car not found.";

            var carResult = car.Reserve();

            if (carResult.IsFailure)
                return carResult.Error!;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
