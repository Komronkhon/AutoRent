using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Reservations.CompleteReservation
{
    public sealed record CompleteReservationCommand(Guid ReservationId) : IRequest<_Result>;
}
