using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Users.DTOs;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Common;
using CarRental.Domain.ValueObjects;
using MediatR;

namespace CarRental.Application.Features.Commands.Users.UpdateUser
{
    public sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, _Result<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result<UserResponse>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByGuidAsync(command.UserId, cancellationToken);

            if (user is not null)
                return "User not found";

            var fullName = FullName.Create(command.FullName);

            if (fullName.IsFailure)
                return fullName.Error!;

            var email = Email.Create(command.Email);

            if (email.IsFailure)
                return email.Error!;

            var phone = PhoneNumber.Create(command.PhoneNumber);

            if (phone.IsFailure)
                return phone.Error!;

            var passport = PassportNumber.Create(command.PassportNumber);

            if (passport.IsFailure)
                return passport.Error!;

            var result = user!.Update(
                fullName.Value,
                email.Value,
                phone.Value,
                passport.Value
            );

            if (result.IsFailure)
                return result.Error!;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UserResponse(
                user.Id,
                user.FullName.Value,
                user.Email.Value,
                user.PhoneNumber.Value,
                user.PassportNumber.Value,
                user.CreatedAt
            );
        }
    }
}
