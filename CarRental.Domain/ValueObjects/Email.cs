using CarRental.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; private set; } = null!;

        private Email() { }

        private Email(string value)
        {
            Value = value;
        }

        public static _Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return "Email cannot be empty.";

            if (!email.Contains("@"))
                return "Invalid email format.";

            return new Email(email);
        }
    }
}
