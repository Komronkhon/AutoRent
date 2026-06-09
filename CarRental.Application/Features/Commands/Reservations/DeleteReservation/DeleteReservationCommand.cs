using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Reservations.DeleteReservation
{
    public sealed record DeleteReservationCommand(Guid ReservationId) : IRequest<_Result>;
}
