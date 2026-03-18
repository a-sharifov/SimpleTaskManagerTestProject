using Domain.SharedKernel.Paginations;
using Domain.TaskModelAggregate.Enumerations;
using Domain.TaskModelAggregate.Ids;
using Domain.TaskModelAggregate.Projections;

namespace Domain.TaskModelAggregate.Repositories;

public interface ITaskModelRepository
{
    Task AddAsync(TaskModel taskModel, CancellationToken cancellationToken = default);
    Task UpdateAsync(TaskModel taskModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(TaskModel taskModel, CancellationToken cancellationToken = default);
    Task<TaskModel?> GetByIdAsync(TaskModelId id, CancellationToken cancellationToken = default);
    Task<TaskModelDetailsProjection?> GetDetailsAsync(TaskModelId id, CancellationToken cancellationToken = default);
    Task<PagedList<TaskModelSummary>> GetPageSummaryAsync(
        int pageNumber,
        int pageSize,
        TaskModelOrderBy orderBy,
        CancellationToken cancellationToken = default
    );
}
