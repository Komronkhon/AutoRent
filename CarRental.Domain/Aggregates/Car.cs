using CarRental.Domain.Common;
using CarRental.Domain.Enums;
using CarRental.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CarRental.Domain.Aggregates
{
    public sealed class Car : BaseAuditableEntity
    {
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public decimal PricePerDay { get; private set; }
        public CarStatus Status { get; private set; }

        private Car() {}

        private Car(string brand, string model, int year, decimal pricePerDay)
        {
            Brand = brand.Trim();
            Model = model.Trim();
            Year = year;
            PricePerDay = pricePerDay;
            Status = CarStatus.Available;
        }

        public static _Result<Car> Create(string brand, string model, int year, decimal pricePerDay)
        {
            if (string.IsNullOrWhiteSpace(brand))
                return "Brand cannot be empty";

            if (string.IsNullOrWhiteSpace(model))
                return "Model cannot be empty";

            if (year < 1900)
                return "Invalid year";

            if (pricePerDay <= 0)
                return "Price per day must be greater than zero";

            return new Car(brand, model, year, pricePerDay);
        }

        public _Result Reserve()
        {
            if (Status == CarStatus.Reserved)
                return "Car is already reserved.";

            Status = CarStatus.Reserved;

            return _Result.Success();
        }

        public _Result MakeAvailable()
        {
            Status = CarStatus.Available;

            return _Result.Success();
        }
        public _Result MarkUnavailable()
        {
            Status = CarStatus.Unavailable;

            return _Result.Success();
        }
    }
}
