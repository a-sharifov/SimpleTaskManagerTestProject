using Application.Common.CQRS.Commands;
using Domain.TaskModelAggregate.Enumerations;
using Domain.TaskModelAggregate.Ids;
using Domain.TaskModelAggregate.Repositories;
using Domain.TaskModelAggregate.ValueObjects;
using Persistence.UnitOfWorks;

namespace Application.TaskModels.Commands.Update;

internal sealed class UpdateTaskModelCommandHandler(
    ITaskModelRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateTaskModelCommand>
{
    private readonly ITaskModelRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
        
    public async Task<Result> Handle(UpdateTaskModelCommand request, CancellationToken cancellationToken)
    {
        var taskModel = await _repository.GetByIdAsync(new TaskModelId(request.Id), cancellationToken);
        if (taskModel is null)
            return Result.NotFound();

        var titleResult = TaskModelTitle.Create(request.Title);
        var descriptionResult = TaskModelDescription.Create(request.Description);
        var deadlineResult = TaskModelDeadline.Create(request.Deadline, DateTime.UtcNow);

        var errors = new List<ValidationError>();
        if (!titleResult.IsSuccess) errors.AddRange(titleResult.ValidationErrors);
        if (!descriptionResult.IsSuccess) errors.AddRange(descriptionResult.ValidationErrors);
        if (!deadlineResult.IsSuccess) errors.AddRange(deadlineResult.ValidationErrors);

        if (errors.Count > 0)
            return Result.Invalid(errors);

        var taskModelUpdateResult = taskModel.Update(
            titleResult.Value, 
            descriptionResult.Value, 
            TaskModelPriority.FromName(request.Priority), 
            deadlineResult.Value, 
            request.IsCompleted);

        if (!taskModelUpdateResult.IsSuccess)
            return Result.Invalid(taskModelUpdateResult.ValidationErrors);

        await _repository.UpdateAsync(taskModel, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
