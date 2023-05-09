using Project.Domain.Common.Premitives;

namespace Project.Domain.MenuAggregate.Events;

public record MenuCreated(Menu Menu) : IDomainEvent;