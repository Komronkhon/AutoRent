using CarRental.Application.Abstractions;
using CarRental.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Infrastructure.Persistence.Repositories
{
    public sealed class PaymentRepository : IPaymentRepository
    {
        private readonly CarRentalDbContext _context;

        public PaymentRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment payment, CancellationToken cancellationToken)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task<List<Payment>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Payments.ToListAsync(cancellationToken);
        }

        public async Task<Payment?> GetByGuidAsync(Guid paymentId, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(x => x.Id == paymentId, cancellationToken);

            return payment;
        }
    }
}
