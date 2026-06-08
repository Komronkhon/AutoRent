using CarRental.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.ValueObjects
{
    public sealed class PhoneNumber
    {
        public string Value { get; }

        private PhoneNumber() { }
        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static _Result<PhoneNumber> Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return "Phone number cannot be empty.";

            if (!phoneNumber.All(char.IsDigit))
                return "Phone number must contain only digits.";

            if (phoneNumber.Length != 9)
                return "Phome number must be 9 characters.";

            return new PhoneNumber(phoneNumber);
        }
    }
}