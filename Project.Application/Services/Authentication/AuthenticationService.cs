using Project.Application.Common.Interfaces.Authentication;

namespace Project.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Login(string Email, string Password)
    {

        return new AuthenticationResult(new Guid(), "FirstName", "LastName", "Email", "Token");
    }

    public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
    {
        /*
          * TODO check if user already exists
          * Create user (generate unique ID)
          * Create a token
         */
        var userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, FirstName, LastName);

        return new AuthenticationResult(userId, FirstName, LastName, Email, token);
    }
}
