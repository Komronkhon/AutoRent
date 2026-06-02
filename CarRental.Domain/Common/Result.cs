using CarRental.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.Common
{
    public class _Result
    {
        public string? Error { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;

        public _Result(bool isSuccess, string? error)
        {
            if (isSuccess && error is not null)
                throw new InvalidOperationException("A successful result cannot have an error message.");

            if (isSuccess == false && error is null)
                throw new InvalidOperationException("A failure result must have an error message.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static _Result Success()
        {
            return new(true, null);
        }

        public static _Result Failure(string error) => new(false, error);

        public static implicit operator _Result(string error) => new(false, error);
    }

    public class _Result<TValue> : _Result
    {
        private readonly TValue _value;

        public _Result(TValue value, bool isSuccess, string? error) : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("Cannot access the value of a failed result.");

        public static _Result<TValue> Success(TValue value) => new(value, true, null);

        public static _Result<TValue> Failure(string error) => new(default!, false, error);

        public static implicit operator _Result<TValue>(TValue value) => new(value, true, null);

        public static implicit operator _Result<TValue>(string error) => new(default!, false, error);
    }
}