using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Reservation.DTOs;
using CarRental.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Reservation.GetReservations
{
    public sealed class GetReservationsHandler
        : IRequestHandler<GetReservationsQuery, List<ReservationResponse>>
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationsHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<List<ReservationResponse>> Handle(
            GetReservationsQuery request,
            CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllAsync(cancellationToken);

            return reservations
                .Select(x => new ReservationResponse(
                    x.Id,
                    x.UserId,
                    x.CarId,
                    x.RentalPeriod.StartDate,
                    x.RentalPeriod.EndDate,
                    x.Status,
                    x.CreatedAt))
                .ToList();
        }
    }
}
