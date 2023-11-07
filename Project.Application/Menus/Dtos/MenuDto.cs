using Project.Domain.Common.ValueObjects;
using Project.Domain.DinnerAggregate.ValueObjects;
using Project.Domain.HostAggregate.ValueObjects;
using Project.Domain.MenuAggregate.Entities;
using Project.Domain.MenuReviewAggregate.ValueObjects;

namespace Project.Application.Menus.Dtos;

public class MenuDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AverageRatingDto? AverageRating { get; set; }
    public Guid HostId { get; set; }
    public List<MenuSectionDto>? Sections { get; set; }
    public List<Guid>? DinnerIds { get; set; }
    public List<Guid>? MenuReviewsIds { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}
