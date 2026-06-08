using CarRental.Application.Abstractions;
using CarRental.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Infrastructure.Persistence.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly CarRentalDbContext _context;

        public UserRepository(CarRentalDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Value == email, cancellationToken);
        }

        public async Task<User?> GetByPassportAsync(string passport, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.PassportNumber.Value == passport, cancellationToken);
        }
        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber.Value == phoneNumber, cancellationToken);
        }

        public async Task<User?> GetByGuidAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
