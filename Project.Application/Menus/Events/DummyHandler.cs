using MediatR;
using Project.Domain.MenuAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Menus.Events;

public class DummyHandler : INotificationHandler<MenuCreated>
{

    public Task Handle(MenuCreated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
