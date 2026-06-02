using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Payments.ConfirmPayment
{
    public sealed record ConfirmPaymentCommand(Guid PaymentId) : IRequest<_Result>;
}
