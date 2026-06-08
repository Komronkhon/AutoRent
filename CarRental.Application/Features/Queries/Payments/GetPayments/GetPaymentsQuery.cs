using CarRental.Application.Features.Queries.Payments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Payments.GetPayment
{
    public sealed record GetPaymentsQuery : IRequest<List<PaymentResponse>>;
}
