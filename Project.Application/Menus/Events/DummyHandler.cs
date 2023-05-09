using MediatR;
using Project.Domain.MenuAggregate.Events;


namespace Project.Application.Menus.Events;

public class DummyHandler : INotificationHandler<MenuCreated>
{

    public Task Handle(MenuCreated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
