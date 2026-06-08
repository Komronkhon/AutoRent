using CarRental.Application.Abstractions;
using CarRental.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Infrastructure.Persistence.Repositories
{
    public sealed class CarRepository : ICarRepository
    {
        private readonly CarRentalDbContext _context;

        public CarRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllAsync(CancellationToken cancellation)
        {
            return await _context.Cars.ToListAsync(cancellation);
        }
        
        public async Task<Car?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Cars.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        
        public async Task AddAsync(Car car, CancellationToken cancellationToken)
        {
            await _context.Cars.AddAsync(car, cancellationToken);
        }

    }
}
