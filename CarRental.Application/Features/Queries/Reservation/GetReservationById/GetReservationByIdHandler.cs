using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Reservation.DTOs;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Reservation.GetReservationById
{
    public sealed class GetReservationByIdHandler
        : IRequestHandler<GetReservationByIdQuery, _Result<ReservationResponse>>
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationByIdHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<_Result<ReservationResponse>> Handle(
            GetReservationByIdQuery request,
            CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByGuidAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
                return "Reservation not found.";

            return new ReservationResponse(
                reservation.Id,
                reservation.UserId,
                reservation.CarId,
                reservation.RentalPeriod.StartDate,
                reservation.RentalPeriod.EndDate,
                reservation.Status,
                reservation.CreatedAt);
        }
    }
}
