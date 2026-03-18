namespace Domain.TaskModelAggregate.Enumerations;

public sealed class TaskModelStatus(string name, int value) : SmartEnum<TaskModelStatus>(name, value)
{
    public static readonly TaskModelStatus Done = new("Done", 1);
    public static readonly TaskModelStatus Overdue = new("Overdue", 2);
    public static readonly TaskModelStatus Urgent = new("Urgent", 3);
    public static readonly TaskModelStatus Active = new("Active", 4);
}
