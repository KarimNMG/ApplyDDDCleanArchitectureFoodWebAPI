using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Users.Commands.UpdateUser;
using Project.Contracts.Users.UpdateUser;

namespace Project.WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        public UserController(
            IMapper mapper,
            ISender sender)
        {
            _mapper = mapper;
            _sender = sender;
        }


        [HttpPost("Update/{Id}")]
        public async Task<IActionResult> UpdateUser(
            Guid userId,
            UpdateUserRequest request)
        {
            var command = _mapper.Map<UpdateUserCommand>((request, userId));

            var commandResult = await _sender.Send(command);

            return commandResult.Match(
                    userId => Ok(userId),
                    erros => Problem(erros));
        }
    }
}