using Application.Common.CQRS.Queries;
using Domain.TaskModelAggregate.Enumerations;

namespace Application.TaskModels.Queries.PrioritiyTokens;

internal sealed class TaskModelPriorityTokensQueryHandler : IQueryHandler<TaskModelPriorityTokensQuery, IEnumerable<string>>
{
    public Task<Result<IEnumerable<string>>> Handle(TaskModelPriorityTokensQuery request, CancellationToken cancellationToken)
    {
        var tokens = TaskModelPriority.List.Select(t => t.Name).AsEnumerable();

        return Task.FromResult(Result.Success(tokens));
    }
}
