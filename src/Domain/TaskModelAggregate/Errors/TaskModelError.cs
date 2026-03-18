namespace Domain.TaskModelAggregate.Errors;

public static class TaskModelError
{
    public static readonly ValidationError InvalidTitle =
        new("TaskModelError.InvalidTitle", "Title cannot be null, empty, or whitespace.");

    public static ValidationError InvalidTitleMaxSize(int maxSize) =>
        new("TaskModelError.InvalidTitleMaxSize", $"Title cannot exceed {maxSize} characters.");

    public static ValidationError InvalidDescriptionMaxSize(int maxSize) =>
        new("TaskModelError.InvalidDescriptionMaxSize", $"Description cannot exceed {maxSize} characters.");

    public static ValidationError InvalidDeadlineInThePast(DateTime value) =>
        new("TaskModelError.InvalidDeadlineInThePast", $"Deadline cannot be in the past: {value}.");
}
