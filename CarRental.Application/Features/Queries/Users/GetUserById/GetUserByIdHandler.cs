using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Users.DTOs;
using CarRental.Domain.Aggregates;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Users.GetUserById
{
    public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, _Result<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<_Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByGuidAsync(request.UserId, cancellationToken);

            if (user is null)
                return "User not found.";

            return new UserResponse(
                user.Id,
                user.FullName.Value,
                user.Email.Value,
                user.PhoneNumber.Value,
                user.PassportNumber.Value,
                user.CreatedAt);
        }
    }
}
