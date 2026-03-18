using Application.Common.CQRS.Commands;
using Domain.TaskModelAggregate.Ids;
using Domain.TaskModelAggregate.Repositories;
using Persistence.UnitOfWorks;

namespace Application.TaskModels.Commands.Delete;

internal sealed class DeleteTaskModelCommandHandler(
    ITaskModelRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteTaskModelCommand>
{
    private readonly ITaskModelRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(DeleteTaskModelCommand request, CancellationToken cancellationToken)
    {
        var id = new TaskModelId(request.Id);
        var taskModel = await _repository.GetByIdAsync(id, cancellationToken);

        if (taskModel is null)
            return Result.NotFound();

        await _repository.DeleteAsync(taskModel, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
