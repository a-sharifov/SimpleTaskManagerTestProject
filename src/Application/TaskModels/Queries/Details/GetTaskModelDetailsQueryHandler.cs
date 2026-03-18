using Application.Common.CQRS.Queries;
using Domain.TaskModelAggregate.Ids;
using Domain.TaskModelAggregate.Projections;
using Domain.TaskModelAggregate.Repositories;

namespace Application.TaskModels.Queries.Details;

internal sealed class GetTaskModelDetailsQueryHandler(
    ITaskModelRepository repository) : IQueryHandler<GetTaskModelDetailsQuery, TaskModelDetailsProjection>
{
    private readonly ITaskModelRepository _repository = repository;
    public async Task<Result<TaskModelDetailsProjection>> Handle(
        GetTaskModelDetailsQuery request, CancellationToken cancellationToken)
    {
        var id = new TaskModelId(request.Id);

        var projection = await _repository.GetDetailsAsync(
            id, cancellationToken);

        if (projection is null)
            return Result<TaskModelDetailsProjection>.NotFound();

        return projection;
    }
}
