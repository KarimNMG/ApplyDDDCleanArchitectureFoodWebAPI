
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces.UnitOfWorks;
using Project.Infrastructure.Presistance;

namespace Project.Infrastructure.Commons;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;

    public UnitOfWork(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<ErrorOr<long?>> SaveChangesAsync()
    {
        try
        {
            return await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Error.Custom(0, "ChangesNotSaved:", "Could not save changes");
        }
    }
}