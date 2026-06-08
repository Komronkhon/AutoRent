using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Users.DTOs;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Common;
using CarRental.Domain.ValueObjects;
using MediatR;

namespace CarRental.Application.Features.Commands.Users.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, _Result<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result<UserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if(existingUser is not null)
            {
                return "Email already exists";
            }

            var existingPassport = await _userRepository.GetByPassportAsync(request.PassportNumber, cancellationToken);

            if (existingPassport is not null)
            {
                return "Passport already exists";
            }

            var existingPhoneNumber = await _userRepository.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken);

            if (existingPhoneNumber is not null)
            {
                return "Phone Number already exists";
            }

            var fullNameResult = FullName.Create(request.FullName);

            if(fullNameResult.IsFailure)
                return fullNameResult.Error!;

            var emailResult = Email.Create(request.Email);

            if(emailResult.IsFailure)
                return emailResult.Error!;

            var phoneResult = PhoneNumber.Create(request.PhoneNumber);

            if(phoneResult.IsFailure)
                return phoneResult.Error!;

            var passportResult = PassportNumber.Create(request.PassportNumber);

            if(passportResult.IsFailure)
                return passportResult.Error!;

            var userResult = User.Create(
                fullNameResult.Value,
                emailResult.Value,
                phoneResult.Value,
                passportResult.Value
            );

            if(userResult.IsFailure)
                return userResult.Error!;

            await _userRepository.AddAsync(userResult.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UserResponse(
                userResult.Value.Id,
                userResult.Value.FullName.Value,
                userResult.Value.Email.Value,
                userResult.Value.PhoneNumber.Value,
                userResult.Value.PassportNumber.Value,
                userResult.Value.CreatedAt
            );
        }
    }
}
