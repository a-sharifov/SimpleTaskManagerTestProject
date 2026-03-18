using Ardalis.Specification;
using Domain.TaskModelAggregate.Enumerations;

namespace Domain.TaskModelAggregate.Specifications;

public sealed class TaskModelOrderBySpec : Specification<TaskModel>
{
    public TaskModelOrderBySpec(TaskModelOrderBy orderByToken)
    {
        switch (orderByToken.Name)
        {
            case nameof(TaskModelOrderBy.DeadlineAsc):
                Query.OrderBy(t => t.Deadline);
                break;
            case nameof(TaskModelOrderBy.DeadlineDesc):
                Query.OrderByDescending(t => t.Deadline);
                break;
            case nameof(TaskModelOrderBy.PriorityAsc):
                Query.OrderBy(t => t.Priority);
                break;
            case nameof(TaskModelOrderBy.PriorityDesc):
                Query.OrderByDescending(t => t.Priority);
                break;
        }

    }
}
