using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Reservations.ConfirmReservation
{
    public sealed record ConfirmReservationCommand(Guid ReservationId) : IRequest<_Result>;
}
