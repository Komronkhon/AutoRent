using CarRental.Application.Features.Queries.Reservation.DTOs;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Reservation.GetReservationById
{
    public sealed record GetReservationByIdQuery(Guid ReservationId)
        : IRequest<_Result<ReservationResponse>>;
}
