﻿
using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces.Authentication;
using Project.Application.Common.Interfaces.Presistance;
using Project.Domain.Entities;
using Project.Domain.Common.Errors;
using Project.Application.Authentication.Common;

namespace Project.Application.Authentication.Queries.Login;

internal sealed class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Now we have a service can return valid result or single error or multiple errors
        var user = returnUser(query.email) as User;
        if (user is null)
            return DomainErrors.Authentication.InvlaidCredentials;
        if (user.Password != query.password)
            return new[] { DomainErrors.Authentication.InvlaidCredentials };
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(new User
        {
            Id = user.Id,
            FirstName = user.FirstName!,
            LastName = user.LastName!,
            Email = user.Email!,
        }, token);
    }

    private User? returnUser(string email) => _userRepository.GetUserByEmailAsync(email);
}
