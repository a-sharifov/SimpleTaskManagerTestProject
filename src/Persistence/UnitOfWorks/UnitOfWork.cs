using Persistence.DbContexts;

namespace Persistence.UnitOfWorks;

public sealed class UnitOfWork(TaskManagerDbContext context) : IUnitOfWork
{
    public async Task Commit(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);
}
