using CarRental.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.ValueObjects
{
    public record RentalPeriod
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        private RentalPeriod() { }

        private RentalPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public static _Result<RentalPeriod> Create(DateTime startDate, DateTime endDate)
        {
            if (startDate.Date < DateTime.UtcNow.Date)
                return "Start date cannot be in the past.";

            if (startDate >= endDate)
                return "End date must be greater than start date.";

            return new RentalPeriod(startDate, endDate);
        }
    }
}
