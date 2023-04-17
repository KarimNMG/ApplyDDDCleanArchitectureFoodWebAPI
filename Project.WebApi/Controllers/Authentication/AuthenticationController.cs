using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Authentication.Commands.Register;
using Project.Application.Authentication.Common;
using Project.Application.Authentication.Queries.Login;
using Project.Contracts.Authentication;
using Project.Domain.Common.Errors;

namespace Project.WebApi.Controllers.Authentication
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(
            IMediator mediator,
            IMapper mapper)
        {
            this._mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var command = _mapper.Map<RegisterCommand>(registerRequest);

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(
                authRes => Ok(_mapper.Map<AuthenticationResult>(authRes)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var query = _mapper.Map<LoginQuery>(loginRequest);
            var authResult = await _mediator.Send(query);


            if (authResult.IsError && authResult.FirstError == DomainErrors.Authentication.InvlaidCredentials)
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authResult.FirstError.Description);
            }

            return authResult.Match(
                authRes => Ok(_mapper.Map<AuthenticationResponse>(authRes)),
                errors => Problem(errors));
        }
    }
}