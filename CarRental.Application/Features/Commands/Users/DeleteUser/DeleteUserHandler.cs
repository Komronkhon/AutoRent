using CarRental.Application.Abstractions;
using CarRental.Application.Features.Commands.Users.RegisterUser;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Users.DeleteUser
{
    public sealed class DeletePaymentHandler : IRequestHandler<DeleteCarCommand, _Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePaymentHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByGuidAsync(request.UserId, cancellationToken);

            if (user is null)
                return "User not found";

            user.Delete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
