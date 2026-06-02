using CarRental.Application.Abstractions;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Payments.CreatePayment
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, _Result<Guid>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePaymentHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = Payment.Create(request.ReservationId, request.Amount, request.PaymentType);

            if (payment.IsFailure)
                return payment.Error!;

            await _paymentRepository.AddAsync(payment.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return payment.Value.Id;
        }
    }
}
