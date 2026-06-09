using CarRental.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Abstractions
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllAsync(CancellationToken cancellationToken);
        Task<Car?> GetByGuidAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Car car, CancellationToken cancellationToken);
    }
}
