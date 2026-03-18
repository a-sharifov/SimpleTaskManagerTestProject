using Application.Common.CQRS.Commands;
using Domain.TaskModelAggregate.Ids;
using Domain.TaskModelAggregate.Repositories;
using Persistence.UnitOfWorks;

namespace Application.TaskModels.Commands.Delete;

internal sealed class DeleteTaskModelCommandHandler(
    ITaskModelRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteTaskModelCommand>
{
    public async Task<Result> Handle(DeleteTaskModelCommand request, CancellationToken cancellationToken)
    {
        var id = new TaskModelId(request.Id);
        var taskModel = await repository.GetByIdAsync(id, cancellationToken);

        if (taskModel is null)
            return Result.NotFound();

        await repository.DeleteAsync(taskModel, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
