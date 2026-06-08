using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Payments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Payments.GetPayment
{
    public sealed class GetPaymentsHandler : IRequestHandler<GetPaymentsQuery, List<PaymentResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentsHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<PaymentResponse>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetAllAsync(cancellationToken);

            var response = payments.Select(p => new PaymentResponse
            (
                p.Id,
                p.ReservationId,
                p.Amount,
                p.Status,
                p.Type,
                p.CreatedAt
            )).ToList();

            return response;
        }
    }
}
