using CarRental.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.ValueObjects
{
    public sealed class FullName
    {
        public string Value { get; }

        private FullName(string value)
        {
            Value = value;
        }

        public static _Result<FullName> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return "Email cannot be empty.";

            if (fullName.Trim().Length < 3)
                return "Full name must contain at least 3 characters.";

            return new FullName(email);
        }
    }
}
