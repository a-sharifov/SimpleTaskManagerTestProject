using Ardalis.Specification.EntityFrameworkCore;
using Domain.SharedKernel.Paginations;
using Domain.TaskModelAggregate;
using Domain.TaskModelAggregate.Enumerations;
using Domain.TaskModelAggregate.Ids;
using Domain.TaskModelAggregate.Projections;
using Domain.TaskModelAggregate.Repositories;
using Domain.TaskModelAggregate.Specifications;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public sealed class TaskModelRepository(TaskManagerDbContext dbContext) : ITaskModelRepository
{
    private readonly TaskManagerDbContext _dbContext = dbContext;

    public Task AddAsync(TaskModel taskModel, CancellationToken cancellationToken = default)
    {
        _dbContext.Add(taskModel);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TaskModel taskModel, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(taskModel);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(TaskModel taskModel, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(taskModel);
        return Task.CompletedTask;
    }

    public async Task<TaskModel?> GetByIdAsync(TaskModelId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.TaskModels.FindAsync(
            [id, cancellationToken], cancellationToken: cancellationToken);
    }

    public async Task<TaskModelDetailsProjection?> GetDetailsAsync(TaskModelId id, CancellationToken cancellationToken = default) =>
        await _dbContext.TaskModels
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TaskModelDetailsProjection(
                t.Id.Value,
                t.Title.Value,
                t.Description.Value,
                t.Priority.Name,
                t.Deadline.Value,
                t.IsCompleted,
                t.Status.Name))
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<PagedList<TaskModelSummary>> GetPageSummaryAsync(
        int pageNumber, 
        int pageSize, 
        TaskModelOrderBy orderBy,
        CancellationToken cancellationToken = default)
    {
        var totalItems = await GetCountAsync(cancellationToken);

        if (totalItems == 0)
            return PagedList<TaskModelSummary>.Empty(pageNumber, pageSize);

        var items = await _dbContext.TaskModels
            .AsNoTracking()
            .WithSpecification(new TaskModelOrderBySpec(orderBy))
            .Select(t => new TaskModelSummary(
                t.Id.Value,
                t.Title.Value,
                t.Priority.Name,
                t.Deadline.Value,
                t.IsCompleted,
                t.Status.Name
                ))
            .Skip(PagedList<TaskModel>.GetSkip(pageNumber, pageSize))
            .Take(PagedList<TaskModel>.GetTake(pageSize))
            .ToListAsync(cancellationToken);


        return new PagedList<TaskModelSummary>(
            items, 
            totalItems, 
            pageNumber, 
            pageSize);
    }

    private async Task<int> GetCountAsync(CancellationToken cancellationToken = default) =>
       await  _dbContext.TaskModels
            .AsNoTracking()
            .CountAsync(cancellationToken);

}
