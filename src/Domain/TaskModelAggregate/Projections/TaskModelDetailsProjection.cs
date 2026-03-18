namespace Domain.TaskModelAggregate.Projections;

public sealed record TaskModelDetailsProjection(
    Guid Id,
    string Title,
    string Description,
    string Priority,
    DateTime Deadline,
    bool IsCompleted,
    string Status
);
