using CarRental.Domain.Common;
using CarRental.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.Aggregates
{
    public sealed class Payment : BaseAuditableEntity
    {
        public Guid ReservationId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public PaymentType Type { get; private set; }

        private Payment() { }

        private Payment(Guid reservationId, decimal amount, PaymentType type)
        {
            ReservationId = reservationId;
            Amount = amount;
            Type = type;
            Status = PaymentStatus.Pending;
        }

        public static _Result<Payment> Create(Guid reservationId, decimal amount, PaymentType type)
        {
            if (string.IsNullOrEmpty(reservationId.ToString()))
                return "ReservationId must be greater than zero.";

            if (amount <= 0)
                return "Amount must be greater than zero.";

            return new Payment(reservationId, amount, type);
        }

        public _Result Confirm()
        {
            if (Status != PaymentStatus.Pending)
                return "Only pending payment can be confirmed.";

            Status = PaymentStatus.Paid;

            return _Result.Success();
        }

        public _Result Fail()
        {
            if (Status != PaymentStatus.Pending)
                return "Only pending payment can be failed.";

            Status = PaymentStatus.Failed;

            return _Result.Success();
        }

        public _Result Refund()
        {
            if (Status != PaymentStatus.Paid)
                return "Only paid payment can be refunded.";

            Status = PaymentStatus.Refunded;

            return _Result.Success();
        }
        public _Result Delete()
        {
            MarkAsDeleted();
            return _Result.Success();
        }
    }
}
