using CarRental.Application.Abstractions;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CarRental.Application.Features.Commands.Payments.RefundPayment
{
    public class RefundPaymentHandler : IRequestHandler<RefundPaymentCommand, _Result>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RefundPaymentHandler(
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

        public async Task<_Result> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByGuidAsync(request.PaymentId, cancellationToken);

            if (payment is null)
                return "Payment not found.";

            var result = payment.Refund();

            if (result.IsFailure)
                return result.Error!;

            var reservation = await _reservationRepository.GetByGuidAsync(payment.ReservationId, cancellationToken);

            if (reservation is null)
                return "Reservation not found.";

            var reservationResult = reservation.Cancel();

            if(reservationResult.IsFailure)
                return reservationResult.Error!;

            var car = await _carRepository.GetByGuidAsync(reservation.CarId, cancellationToken);

            if (car is null)
                return "Car not found.";

            var carResult = car.MakeAvailable();

            if(carResult.IsFailure)
                return carResult.Error!;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
