using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Reservations.CreateReservation
{
    public sealed record CreateReservationCommand(
        Guid UserId,
        Guid CarId,
        DateTime StartDate,
        DateTime EndDate
    ) : IRequest<_Result<Guid>>;
}
