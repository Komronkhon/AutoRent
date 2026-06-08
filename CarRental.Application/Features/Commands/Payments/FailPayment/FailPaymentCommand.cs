using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Payments.FailPayment
{
    public sealed record ConfirmPaymentCommand(Guid PaymentId) : IRequest<_Result>;
}
