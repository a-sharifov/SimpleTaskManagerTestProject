using Application.Common.CQRS.Queries;

namespace Application.TaskModels.Queries.OrderByTokens;

public sealed record TaskModelOrderByTokensQuery : IQuery<IEnumerable<string>>;
