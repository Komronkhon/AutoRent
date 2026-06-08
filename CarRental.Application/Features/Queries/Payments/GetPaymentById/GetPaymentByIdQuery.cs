using CarRental.Application.Features.Queries.Payments.DTOs;
using CarRental.Application.Features.Queries.Reservation.DTOs;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Payments.GetReservationById
{
    public sealed record GetPaymentByIdQuery(Guid PaymentId) : IRequest<_Result<PaymentResponse>>;
}
