using CarRental.Domain.Common;
using CarRental.Domain.Enums;
using CarRental.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.Aggregates
{
    public sealed class Reservation : BaseAuditableEntity
    {
        public Guid UserId{ get; private set; }
        public Guid CarId { get; private set; }
        public RentalPeriod RentalPeriod{ get; private set; }
        public ReservationStatus Status { get; private set; }

        private Reservation() { }

        private Reservation(Guid userId, Guid carId, RentalPeriod rentalPeriod)
        {
            UserId = userId;
            CarId = carId;
            RentalPeriod = rentalPeriod;
            Status = ReservationStatus.Pending;
        }

        public static _Result<Reservation> Create(Guid userId, Guid carId, RentalPeriod rentalPeriod)
        {
            if(userId == Guid.Empty)
                return "UserId cannot be empty.";

            if(carId == Guid.Empty)
                return "CarId cannot be empty.";

            if (rentalPeriod is null)
                return "Rental period is required.";

            return new Reservation(userId, carId, rentalPeriod);
        }

        public _Result Confirm()
        {
            if (Status != ReservationStatus.Pending)
                return "Only pending reservations can be confirmed.";

            Status = ReservationStatus.Confirmed;

            return _Result.Success();
        }

        public _Result Cancel()
        {
            if (Status == ReservationStatus.Completed)
                return "Completed reservation cannot be cancelled.";

            Status = ReservationStatus.Cancelled;

            return _Result.Success();
        }

        public _Result Complete()
        {
            if (Status != ReservationStatus.Confirmed)
                return "Only confirmed reservations can be completed.";

            Status = ReservationStatus.Completed;

            return _Result.Success();
        }
    }
}
