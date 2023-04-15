using ErrorOr;
using MediatR;
using Project.Application.Authentication.Common;
using Project.Application.Common.Interfaces.Authentication;
using Project.Application.Common.Interfaces.Presistance;
using Project.Domain.Common.Errors;
using Project.Domain.Entities;

namespace Project.Application.Authentication.Commands;

internal sealed class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{

    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = returnUser(command.email);
        if (user is not null)
        {
            return DomainErrors.User.DuplicatEmail;
        }

        user = new User
        {
            Email = command.email,
            FirstName = command.firstName,
            LastName = command.lastName,
            Password = command.password
        };
        var userId = _userRepository.AddAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(new User
        {
            Id = userId,
            FirstName = user?.FirstName!,
            LastName = user?.LastName!,
            Email = user?.Email!
        },
        token);
    }
    private User? returnUser(string email) => _userRepository.GetUserByEmailAsync(email);
}