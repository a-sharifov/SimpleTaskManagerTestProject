using Domain.TaskModelAggregate.Errors;

namespace Domain.TaskModelAggregate.ValueObjects;

public sealed class TaskModelTitle
{
    public string Value { get; }

    public const int MaxLength = 128;

    private TaskModelTitle(string value) =>
        Value = value;

    public static Result<TaskModelTitle> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<TaskModelTitle>.Invalid(
                TaskModelError.InvalidTitle);   

        if (value.Length > MaxLength)
            return Result<TaskModelTitle>.Invalid(
                TaskModelError.InvalidTitleMaxSize(MaxLength));

        return new TaskModelTitle(value);
    }
}
