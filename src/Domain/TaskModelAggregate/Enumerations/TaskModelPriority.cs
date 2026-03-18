namespace Domain.TaskModelAggregate.Enumerations;

public sealed class TaskModelPriority(string name, int value) : SmartEnum<TaskModelPriority>(name, value)
{
    public static readonly TaskModelPriority Low = new("Low", 1);
    public static readonly TaskModelPriority Medium = new("Medium", 2);
    public static readonly TaskModelPriority High = new("High", 3);
}
