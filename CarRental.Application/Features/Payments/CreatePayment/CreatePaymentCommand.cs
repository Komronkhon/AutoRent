using CarRental.Domain.Common;
using CarRental.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Payments.CreatePayment
{
    public sealed record CreatePaymentCommand(
        Guid ReservationId,
        decimal Amount,
        PaymentType PaymentType
    ) : IRequest<_Result<Guid>>;
}
