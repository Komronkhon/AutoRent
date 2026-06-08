using CarRental.Application.Abstractions;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace CarRental.Infrastructure.Persistence.Repositories
{
    public sealed class ReservationRepository : IReservationRepository
    {
        private readonly CarRentalDbContext _context;

        public ReservationRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasOverlappingReservationAsync(
            Guid carId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .AnyAsync(
                    x =>
                        x.CarId == carId &&
                        x.Status != ReservationStatus.Cancelled &&
                        startDate < x.RentalPeriod.EndDate &&
                        endDate > x.RentalPeriod.StartDate,
                    cancellationToken);
        }

        public async Task AddAsync(Reservation reservation, CancellationToken cancellationToken)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task<List<Reservation>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Reservations.ToListAsync(cancellationToken);
        }

        public async Task<Reservation?> GetByIdAsync(Guid reservationId, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(x => x.Id == reservationId, cancellationToken);

            return reservation;
        }
    }
}
