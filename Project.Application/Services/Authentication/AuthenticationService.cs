using Project.Application.Common.Errors;
using Project.Application.Common.Interfaces.Authentication;
using Project.Application.Common.Interfaces.Presistance;
using Project.Domain.Entities;

namespace Project.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string Email, string Password)
    {
        var user = returnUser(Email);
        if (user is null)
            throw new Exception("User with given email does not exists");
        if (user.Password != Password)
            throw new Exception("Invalid Password");
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(new User
        {
            Id = user.Id,
            FirstName = user.FirstName!,
            LastName = user.LastName!,
            Email = user.Email!,
        }, token);
    }

    public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
    {
        var user = returnUser(Email);
        if (user is not null)
        {
            throw new DuplicationEmailException();
        }

        user = new User
        {
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            Password = Password
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