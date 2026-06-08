using CarRental.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User?> GetByPassportAsync(string passport, CancellationToken cancellationToken);
        Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
        Task<User?> GetByGuidAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
