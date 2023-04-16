
using ErrorOr;
using MediatR;
using Project.Domain.MenuAggregate;

namespace Project.Application.Menus.Commands.CreateMenu;

public sealed record CreateMenueCommand(
    string Name,
    string Description,
    string HostId,
    List<MenuSectionCommand> Sections) : IRequest<ErrorOr<Menu>>;

public sealed record MenuSectionCommand(
    string Name,
    string Description,
    List<MenuItemCommand> Items
    );

public sealed record MenuItemCommand(
    string Name,
    string Description);