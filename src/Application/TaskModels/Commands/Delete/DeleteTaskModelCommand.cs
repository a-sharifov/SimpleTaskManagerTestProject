using Application.Common.CQRS.Commands;

namespace Application.TaskModels.Commands.Delete;

public sealed record DeleteTaskModelCommand(Guid Id) : ICommand;
