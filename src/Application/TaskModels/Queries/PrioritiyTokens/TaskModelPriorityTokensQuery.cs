using Application.Common.CQRS.Queries;

namespace Application.TaskModels.Queries.PrioritiyTokens;

public sealed record TaskModelPriorityTokensQuery : IQuery<IEnumerable<string>>;
