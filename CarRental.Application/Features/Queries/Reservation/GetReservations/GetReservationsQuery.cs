using CarRental.Application.Features.Queries.Reservation.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Reservation.GetReservations
{
    public sealed record GetReservationsQuery : IRequest<List<ReservationResponse>>;
}
