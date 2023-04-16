using Project.Domain.Common.Models;
using Project.Domain.MenuAggregate.ValueObjects;


namespace Project.Domain.MenuAggregate.Entities;

public sealed class MenuSection : Entity<MenuSectionId>
{

    private readonly List<MenuItem> _items = new();

    public string Name { get; }
    public string Description { get; }
    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();
    private MenuSection(
        MenuSectionId id,
        string name,
        string description,
        List<MenuItem> items) : base(id)
    {
        Name = name;
        Description = description;
        _items = items;
    }

    public static MenuSection Create(
        string name,
        string Description,
        List<MenuItem> items)
    {
        return new MenuSection(
            MenuSectionId.CreateUnique(),
            name,
            Description,
            items);
    }
}