using Project.Application.Common.Interfaces.UnitOfWorks;
using Project.Domain.Common.Errors;
using Project.Infrastructure.Presistance;

namespace Project.Infrastructure.Commons;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;

    public UnitOfWork(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Result<long?>> SaveChangesAsync()
    {
        try
        {
            return await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<long?>(new Error("ChangesNotSaved:", $"Could not save changes, {ex.Message}"));
        }
    }
}