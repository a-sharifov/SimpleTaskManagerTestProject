using Domain.TaskModelAggregate.Errors;

namespace Domain.TaskModelAggregate.ValueObjects;

public sealed class TaskModelDeadline
{
    public DateTime Value { get; }

    private TaskModelDeadline(DateTime value) =>
        Value = value;
    public static Result<TaskModelDeadline> Create(DateTime value, DateTime now)
    {
        var utcValue = value.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
            : value.ToUniversalTime();

        if (utcValue < now)
            return Result<TaskModelDeadline>.Invalid(
                TaskModelError.InvalidDeadlineInThePast(utcValue));

        return new TaskModelDeadline(utcValue);
    }

    public bool IsExpired(DateTime now) =>
        Value < now;

    public double GetHoursUntilDeadline(DateTime now) =>
        (Value - now).TotalHours;
}
