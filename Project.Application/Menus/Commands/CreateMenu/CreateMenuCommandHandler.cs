using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces.Presistance;
using Project.Domain.HostAggregate.ValueObjects;
using Project.Domain.MenuAggregate;
using Project.Domain.MenuAggregate.Entities;

namespace Project.Application.Menus.Commands.CreateMenu;

internal class CreateMenuCommandHandler : IRequestHandler<CreateMenueCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;
    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }
    public async Task<ErrorOr<Menu>> Handle(
        CreateMenueCommand request,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Create Menu
        var menu = Menu.Create(
            name: request.Name,
            description: request.Description,
            hostId: HostId.CreateUnique(Guid.Parse(request.HostId)),
            sections: request.Sections.ConvertAll(section => MenuSection.Create(
                section.Name,
                section.Description,
                section.Items.ConvertAll(item => MenuItem.Create(
                    item.Name,
                    item.Description)))));

        // Persist Menu

        _menuRepository.Add(menu);

        // Return Menu
        return menu;
    }
}