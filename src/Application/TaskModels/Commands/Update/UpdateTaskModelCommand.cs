using Application.Common.CQRS.Commands;

namespace Application.TaskModels.Commands.Update;

public sealed record UpdateTaskModelCommand(
    Guid Id,
    string Title,
    string Description,
    string Priority,
    DateTime Deadline,
    bool IsCompleted) : ICommand;
    