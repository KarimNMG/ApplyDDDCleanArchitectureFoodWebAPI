using Project.Domain.Common.Models;
using Project.Domain.Common.ValueObjects;
using Project.Domain.DinnerAggregate.ValueObjects;
using Project.Domain.HostAggregate.ValueObjects;
using Project.Domain.MenuAggregate.Entities;
using Project.Domain.MenuAggregate.ValueObjects;
using Project.Domain.MenuReviewAggregate.ValueObjects;

namespace Project.Domain.MenuAggregate;


/// <summary>
/// this is a mune aggregate root for the menu feature
/// </summary>
public sealed class Menu : AggregateRoot<MenuId>
{

    private readonly List<MenuSection> _sections = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewsIds = new();
    private Menu(MenuId id,
        string name,
        string description,
        HostId hostId,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<MenuSection> sections) : base(id)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        _sections = sections;
    }

    public static Menu Create(
        string name,
        string description,
        HostId hostId,
        List<MenuSection> sections)
    {
        return new Menu(
            MenuId.CreateUnique(),
            name,
            description,
            hostId,
            DateTime.UtcNow,
            DateTime.UtcNow,
            sections);
    }

    public string Name { get; }
    public string Description { get; }
    public AverageRating AverageRating { get; }
    public HostId HostId { get; }
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewsIds => _menuReviewsIds.AsReadOnly();
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

}
