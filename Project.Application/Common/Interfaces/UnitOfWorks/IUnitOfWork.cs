using ErrorOr;
using Microsoft.EntityFrameworkCore;
namespace Project.Application.Common.Interfaces.UnitOfWorks;

public interface IUnitOfWork
{
    Task<ErrorOr<long?>> SaveChangesAsync();
}