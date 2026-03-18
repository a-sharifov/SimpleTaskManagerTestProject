
namespace Domain.TaskModelAggregate.Enumerations;

public sealed class TaskModelOrderBy(string name, int value) : SmartEnum<TaskModelOrderBy>(name, value)
{
    public static readonly TaskModelOrderBy DeadlineAsc = new(nameof(DeadlineAsc), 1);
    public static readonly TaskModelOrderBy DeadlineDesc = new(nameof(DeadlineDesc), 2);
    public static readonly TaskModelOrderBy PriorityAsc = new(nameof(PriorityAsc), 3);
    public static readonly TaskModelOrderBy PriorityDesc = new(nameof(PriorityDesc), 4);
}
