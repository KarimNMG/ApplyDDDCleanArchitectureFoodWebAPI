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
    private readonly ISender _mediatorSender;

    public MenusController(IMapper mapper, ISender mediatorSender)
    {
        _mapper = mapper;
        _mediatorSender = mediatorSender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenu(
        CreateMenueRequest menuRequest,
        string hostId)
    {
        var command = _mapper.Map<CreateMenueCommand>((menuRequest, hostId));
        var createMenuResult = await _mediatorSender.Send(command);
        //CreatedAtAction(nameof(GetMunu), new {hostId, menuId = menu.Id}, menu)
        return createMenuResult.Match(
            menu => Ok(_mapper.Map<MenuResponse>(menu)),
            erros => Problem(erros));
    }
}