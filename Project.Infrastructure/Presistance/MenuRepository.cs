using Project.Application.Common.Interfaces.Presistance;
using Project.Domain.MenuAggregate;

namespace Project.Infrastructure.Presistance;

internal class MenuRepository : IMenuRepository
{
    private static readonly List<Menu> _menus = new();
    public MenuRepository()
    {

    }

    public void Add(Menu menu)
    {
        _menus.Add(menu);
    }
}