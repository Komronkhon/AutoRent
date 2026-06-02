using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Reservations.CancelReservation
{
    public sealed record CancelReservationCommand(Guid ReservationId) : IRequest<_Result>;
}
