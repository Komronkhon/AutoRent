using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Payments.RefundPayment
{
    public sealed record RefundPaymentCommand(Guid PaymentId) : IRequest<_Result>;
}
