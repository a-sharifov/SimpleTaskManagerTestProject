using Application.Common.CQRS.Queries;
using Domain.TaskModelAggregate.Projections;

namespace Application.TaskModels.Queries.Details;

public sealed record GetTaskModelDetailsQuery(Guid Id) : IQuery<TaskModelDetailsProjection>;
