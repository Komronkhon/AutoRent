using CarRental.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Abstractions
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment, CancellationToken cancellationToken);
        Task<Payment?> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken);

    }
}
