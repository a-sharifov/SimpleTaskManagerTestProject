namespace Domain.TaskModelAggregate.Projections;

public sealed record TaskModelSummary(
    Guid Id,
    string Title,
    string Priority,
    DateTime Deadline,
    bool IsCompleted,
    string Status
);