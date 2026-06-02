using CarRental.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Abstractions
{
    public interface ICarRepository
    {
        Task AddAsync(Car car, CancellationToken cancellationToken);
        Task<Car?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
