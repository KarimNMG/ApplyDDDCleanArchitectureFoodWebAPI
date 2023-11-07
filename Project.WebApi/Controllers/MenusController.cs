using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Menus.Commands.CreateMenu;
using Project.Contracts.Menus;

namespace Project.WebApi.Controllers;

[Route("hosts/{hostId}/{menus}")]
public class MenusController : ApiController
{
    private readonly IMapper _mapper;

    public MenusController(IMapper mapper, ISender sender) : base(sender)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenu(
        CreateMenueRequest menuRequest,
        string hostId)
    {
        var command = _mapper.Map<CreateMenueCommand>((menuRequest, hostId));
        var createMenuResult = await Sender.Send(command);
        //CreatedAtAction(nameof(GetMunu), new {hostId, menuId = menu.Id}, menu)
        if (createMenuResult.IsFailure)
        {
            return HandleFailure(createMenuResult);
        }
        return CreatedAtAction(nameof(CreateMenu), createMenuResult.Value);
    }
}