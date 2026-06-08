using CarRental.Application.Abstractions;
using CarRental.Application.Features.Queries.Users.DTOs;
using CarRental.Application.Features.Queries.Users.GetUserById;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Queries.Users.GetUsers
{
    public sealed class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);

            return users
                .Select(user => new UserResponse(
                    user.Id,
                    user.FullName.Value,
                    user.Email.Value,
                    user.PhoneNumber.Value,
                    user.PassportNumber.Value,
                    user.CreatedAt))
                .ToList();
        }
    }
}
