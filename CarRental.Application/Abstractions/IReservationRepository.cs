using CarRental.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Abstractions
{
    public interface IReservationRepository
    {
        Task<bool> HasOverlappingReservationAsync(
            Guid carId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken);

        Task AddAsync(Reservation reservation, CancellationToken cancellationToken);
        Task<Reservation?> GetByIdAsync(Guid reservationId, CancellationToken cancellationToken);
    }
}
