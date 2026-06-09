using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Cars.DeleteCar
{
    public sealed record DeleteReservationCommand(Guid CarId) : IRequest<_Result>;
}
