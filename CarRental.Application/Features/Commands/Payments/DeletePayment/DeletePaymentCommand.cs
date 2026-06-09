using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Payments.DeletePayment
{
    public sealed record DeletePaymentCommand(Guid PaymentId) : IRequest<_Result>;
}
