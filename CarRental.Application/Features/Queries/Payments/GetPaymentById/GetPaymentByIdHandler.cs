using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Payments.DTOs;
using CarRental.Application.Features.Queries.Payments.GetReservationById;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Payments.GetPaymentById
{
    public sealed class GetPaymentByIdHandler : IRequestHandler<GetPaymentByIdQuery, _Result<PaymentResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentByIdHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<_Result<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByGuidAsync(request.PaymentId, cancellationToken);

            if (payment is null)
                return "Payment not found.";

            return new PaymentResponse
            (
                payment.Id,
                payment.ReservationId,
                payment.Amount,
                payment.Status,
                payment.Type,
                payment.CreatedAt
            );
        }
    }
}
