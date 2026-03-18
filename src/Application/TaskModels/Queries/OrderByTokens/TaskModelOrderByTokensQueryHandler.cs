using Application.Common.CQRS.Queries;
using Domain.TaskModelAggregate.Enumerations;

namespace Application.TaskModels.Queries.OrderByTokens;

internal sealed class TaskModelOrderByTokensQueryHandler : IQueryHandler<TaskModelOrderByTokensQuery, IEnumerable<string>>
{
    public Task<Result<IEnumerable<string>>> Handle(TaskModelOrderByTokensQuery request, CancellationToken cancellationToken)
    {
        var tokens = TaskModelOrderBy.List.Select(t => t.Name).AsEnumerable();

        return Task.FromResult(Result.Success(tokens));
    }
}