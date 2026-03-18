using Domain.TaskModelAggregate.Errors;

namespace Domain.TaskModelAggregate.ValueObjects;

public sealed class TaskModelDescription
{
    public string Value { get; }

    public const int MaxLength = 512;

    private TaskModelDescription(string value) =>
        Value = value;
    public static Result<TaskModelDescription> Create(string value)
    {
        if (value.Length > MaxLength)
            return Result<TaskModelDescription>.Invalid(
                TaskModelError.InvalidDescriptionMaxSize(MaxLength));

        return new TaskModelDescription(value);
    }
}
