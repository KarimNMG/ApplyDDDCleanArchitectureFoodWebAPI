using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Services.Authentication;
using Project.Contracts.Authentication;

namespace Project.WebApi.Controllers.Authentication
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            ErrorOr<AuthenticationResult> authResult = authenticationService.Register(
                registerRequest.FirstName,
                registerRequest.LastName,
                registerRequest.Email,
                registerRequest.Password);

            return authResult.Match(
                authRes => Ok(MapRegisterResponse(authRes)),
                errors => Problem(errors));
        }

        private AuthenticationResponse MapRegisterResponse(AuthenticationResult result)
        {
            return new AuthenticationResponse
             (
                 result.user.Id,
                 result.user.FirstName!,
                 result.user.LastName!,
                 result.user.Email!,
                 result.Token
              );
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var authResult = authenticationService.Login(
                loginRequest.Email,
                loginRequest.Password);

            return authResult.Match(
                authRes => Ok(MapRegisterResponse(authRes)),
                errors => Problem(errors));
        }
    }
}
