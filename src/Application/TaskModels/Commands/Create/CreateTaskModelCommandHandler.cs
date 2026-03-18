using Application.Common.CQRS.Commands;
using Domain.TaskModelAggregate;
using Domain.TaskModelAggregate.Enumerations;
using Domain.TaskModelAggregate.Ids;
using Domain.TaskModelAggregate.Repositories;
using Domain.TaskModelAggregate.ValueObjects;
using Persistence.UnitOfWorks;

namespace Application.TaskModels.Commands.Create;

internal sealed class CreateTaskModelCommandHandler(
    ITaskModelRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateTaskModelCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTaskModelCommand request, CancellationToken cancellationToken)
    {
        var titleResult = TaskModelTitle.Create(request.Title);
        var descriptionResult = TaskModelDescription.Create(request.Description);
        var deadlineResult = TaskModelDeadline.Create(request.Deadline, DateTime.UtcNow);

        var errors = new List<ValidationError>();
        if (!titleResult.IsSuccess) errors.AddRange(titleResult.ValidationErrors);
        if (!descriptionResult.IsSuccess) errors.AddRange(descriptionResult.ValidationErrors);
        if (!deadlineResult.IsSuccess) errors.AddRange(deadlineResult.ValidationErrors);

        if (errors.Count > 0)
            return Result<Guid>.Invalid(errors);

        var id = TaskModelId.New();

        var taskModelResult = TaskModel.Create(
            id, 
            titleResult.Value, 
            descriptionResult.Value, 
            TaskModelPriority.FromName(request.Priority), 
            deadlineResult.Value);

        if (!taskModelResult.IsSuccess)
            return Result<Guid>.Invalid(taskModelResult.ValidationErrors);

        await repository.AddAsync(taskModelResult.Value, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        return id.Value;
    }
}
