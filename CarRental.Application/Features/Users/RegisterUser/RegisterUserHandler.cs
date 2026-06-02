using CarRental.Application.Abstractions;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Common;
using CarRental.Domain.ValueObjects;
using MediatR;

namespace CarRental.Application.Features.Users.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, _Result<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task<_Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
                request.FullName,
                emailResult.Value,
                phoneResult.Value,
                passportResult.Value
            );

            if(userResult.IsFailure)
                return userResult.Error!;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userResult.Value.Id;
        }
    }
}
