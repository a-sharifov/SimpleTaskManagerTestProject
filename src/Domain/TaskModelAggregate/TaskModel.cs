using Domain.TaskModelAggregate.Enumerations;
using Domain.TaskModelAggregate.Ids;
using Domain.TaskModelAggregate.ValueObjects;

namespace Domain.TaskModelAggregate;

public class TaskModel
{
        public TaskModelId Id { get; private set; }
        public TaskModelTitle Title { get; private set; }
        public TaskModelDescription Description { get; private set; }
        public TaskModelPriority Priority { get; private set; }    
        public TaskModelDeadline Deadline { get; private set; }
        public bool IsCompleted { get; private set; }
        public TaskModelStatus Status => GetStatus();

        private bool IsUrgent => Deadline.GetHoursUntilDeadline(DateTime.UtcNow) < 24;
        private bool IsExpired => Deadline.IsExpired(DateTime.UtcNow);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private TaskModel() { } // Required EF 
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private TaskModel(
        TaskModelId id,
        TaskModelTitle title,
        TaskModelDescription description,
        TaskModelPriority priority,
        TaskModelDeadline deadline,
        bool isCompleted = false)
    {
        Id = id; 
        Title = title;
        Description = description;
        Priority = priority;
        Deadline = deadline;
        IsCompleted = isCompleted;
    }

    public static Result<TaskModel> Create(
        TaskModelId id,
        TaskModelTitle title,
        TaskModelDescription description,
        TaskModelPriority priority,
        TaskModelDeadline deadline)
    {
        // add validation logic, domain event 

        return new TaskModel(
            id, title, description, priority, deadline);
    }

    public Result<TaskModel> Update(
        TaskModelTitle title,
        TaskModelDescription description,
        TaskModelPriority priority,
        TaskModelDeadline deadline,
        bool isCompleted = false)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Deadline = deadline;
        IsCompleted = isCompleted;

        return Result.Success();
    }

    private TaskModelStatus GetStatus()
    {
        if (IsExpired)
            return TaskModelStatus.Overdue;

        if (IsUrgent)
            return TaskModelStatus.Urgent;

        if (IsCompleted)
            return TaskModelStatus.Done;

        return TaskModelStatus.Active;
    }
}
