using CarRental.Domain.Common;
using CarRental.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.Aggregates
{
    public sealed class User : BaseAuditableEntity
    {
        public string FullName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public PassportNumber PassportNumber { get; private set; }

        private User() { }

        private User(
            string fullName,
            Email email,
            PhoneNumber phoneNumber,
            PassportNumber passportNumber)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            PassportNumber = passportNumber;
        }

        public static _Result<User> Create(
            string fullName,
            Email email,
            PhoneNumber phoneNumber,
            PassportNumber passportNumber)
        {
            return new User(fullName, email, phoneNumber, passportNumber);
        }
    }
}
