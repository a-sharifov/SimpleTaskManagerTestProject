namespace Persistence.UnitOfWorks;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken = default);
}