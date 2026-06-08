using CarRental.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.ValueObjects
{
    public sealed class PassportNumber
    {
        public string Value { get; }

        private PassportNumber() { }
        private PassportNumber(string value)
        {
            Value = value;
        }

        public static _Result<PassportNumber> Create(string passportNumber)
        {
            if (string.IsNullOrWhiteSpace(passportNumber))
                return "Passport number cannot be empty.";

            if (passportNumber.Length != 9)
                return "Passport number must be 9 characters.";

            return new PassportNumber(passportNumber);
        }
    }
}
