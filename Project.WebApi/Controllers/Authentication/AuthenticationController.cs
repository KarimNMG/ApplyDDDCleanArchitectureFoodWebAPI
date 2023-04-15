using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Services.Authentication;
using Project.Contracts.Authentication;

namespace Project.WebApi.Controllers.Authentication
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            var authResult = authenticationService.Register(
                registerRequest.FirstName,
                registerRequest.LastName,
                registerRequest.Email,
                registerRequest.Password);

            var response = new AuthenticationResponse
            (
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token
             );


            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var authResult = authenticationService.Login(
                loginRequest.Email,
                loginRequest.Password);

            var response = new AuthenticationResponse
            (
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token
             );
            return Ok(response);
        }
    }
}
