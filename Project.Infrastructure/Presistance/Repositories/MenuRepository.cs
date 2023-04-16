using Project.Application.Common.Interfaces.Presistance;
using Project.Domain.MenuAggregate;

namespace Project.Infrastructure.Presistance.Repositories;

internal class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;
    public MenuRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Menu menu)
    {
        _context.Add(menu);
        _context.SaveChanges();
    }
}