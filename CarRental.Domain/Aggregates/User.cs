using CarRental.Domain.Common;
using CarRental.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.Aggregates
{
    public sealed class User : BaseAuditableEntity
    {
        public FullName FullName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public PassportNumber PassportNumber { get; private set; }

        private User() { }

        private User(
            FullName fullName,
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
            FullName fullName,
            Email email,
            PhoneNumber phoneNumber,
            PassportNumber passportNumber)
        {
            return new User(fullName, email, phoneNumber, passportNumber);
        }

        public _Result Update(
            FullName fullName,
            Email email,
            PhoneNumber phoneNumber,
            PassportNumber passportNumber)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            PassportNumber = passportNumber;

            MarkAsUpdated();

            return _Result.Success();
        }

        public _Result Delete()
        {
            MarkAsDeleted();
            return _Result.Success();
        }
    }
}
