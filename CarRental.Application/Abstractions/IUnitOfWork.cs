using System;
using System.Collections.Generic;
using System.Data;

namespace CarRental.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
