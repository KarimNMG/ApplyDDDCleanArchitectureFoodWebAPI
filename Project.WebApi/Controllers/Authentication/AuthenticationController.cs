using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Authentication.Commands;
using Project.Application.Authentication.Common;
using Project.Application.Authentication.Queries.Login;
using Project.Contracts.Authentication;
using Project.Domain.Common.Errors;

namespace Project.WebApi.Controllers.Authentication
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;

        public AuthenticationController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var command = new RegisterCommand(
                registerRequest.FirstName,
                registerRequest.LastName,
                registerRequest.Email,
                registerRequest.Password);

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(
                authRes => Ok(MapRegisterResponse(authRes)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var query = new LoginQuery(
                loginRequest.Email,
                loginRequest.Password);

            var authResult = await _mediator.Send(query);


            if (authResult.IsError && authResult.FirstError == DomainErrors.Authentication.InvlaidCredentials)
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authResult.FirstError.Description);
            }

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
    }
}
