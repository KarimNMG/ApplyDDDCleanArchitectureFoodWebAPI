using Project.Domain.MenuAggregate;

namespace Project.Application.Common.Interfaces.Presistance;
public interface IMenuRepository
{
    void Add(Menu menu);
}