using CarRental.Domain.Common;
using MediatR;

namespace CarRental.Application.Features.Cars.CreateCar
{
    public sealed record CreateCarCommand(
        string Brand,
        string Model,
        int Year,
        decimal PricePerDay
    ) : IRequest<_Result<Guid>>;
}