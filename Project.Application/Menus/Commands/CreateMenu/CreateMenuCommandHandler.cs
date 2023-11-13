using MediatR;
using Project.Application.Common.Interfaces.Presistance;
using Project.Application.Common.Interfaces.UnitOfWorks;
using Project.Domain.Common.Errors;
using Project.Domain.HostAggregate.ValueObjects;
using Project.Domain.MenuAggregate;
using Project.Domain.MenuAggregate.Entities;

namespace Project.Application.Menus.Commands.CreateMenu;

internal class CreateMenuCommandHandler : IRequestHandler<CreateMenueCommand, Result<Menu>>
{
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateMenuCommandHandler(
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork)
    {
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Menu>> Handle(
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
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        // Return Menu
        return menu;
    }
}