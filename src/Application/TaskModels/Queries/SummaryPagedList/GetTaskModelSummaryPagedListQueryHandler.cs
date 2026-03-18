using Application.Common.CQRS.Queries;
using Domain.SharedKernel.Paginations;
using Domain.TaskModelAggregate.Enumerations;
using Domain.TaskModelAggregate.Projections;
using Domain.TaskModelAggregate.Repositories;

namespace Application.TaskModels.Queries.SummaryPagedList;

internal sealed class GetTaskModelSummaryPagedListQueryHandler(
    ITaskModelRepository repository) : IQueryHandler<GetTaskModelSummaryPagedListQuery, PagedList<TaskModelSummary>>
{
    private readonly ITaskModelRepository _repository = repository;
    public async Task<Result<PagedList<TaskModelSummary>>> Handle(
        GetTaskModelSummaryPagedListQuery request, CancellationToken cancellationToken)
    {
        var orderBy = TaskModelOrderBy.TryFromName(request.OrderBy, out var parsed)
            ? parsed
            : TaskModelOrderBy.DeadlineAsc;

        var page = await _repository.GetPageSummaryAsync(
            request.PageNumber, request.PageSize, orderBy, cancellationToken);

        return page;
    }
}
