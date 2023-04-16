using Project.Domain.Common.Models;
using Project.Domain.MenuAggregate.ValueObjects;

namespace Project.Domain.MenuAggregate.Entities;

public sealed class MenuItem : Entity<MenuItemId>
{
    public string Name { get; }
    public string Description { get; }
    private MenuItem(MenuItemId id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static MenuItem Create(string name, string Description)
    {
        return new MenuItem(MenuItemId.CreateUnique(), name, Description);
    }
}
