using Application.Common.CQRS.Queries;
using Domain.SharedKernel.Paginations;
using Domain.TaskModelAggregate.Projections;

namespace Application.TaskModels.Queries.SummaryPagedList;

public sealed record GetTaskModelSummaryPagedListQuery(
    int PageNumber,
    int PageSize,
    string OrderBy) : IQuery<PagedList<TaskModelSummary>>;
