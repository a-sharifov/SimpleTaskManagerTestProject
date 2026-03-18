using Application.Common.CQRS.Commands;

namespace Application.TaskModels.Commands.Create;

public sealed record CreateTaskModelCommand(
    string Title,
    string Description,
    string Priority,
    DateTime Deadline) : ICommand<Guid>;
