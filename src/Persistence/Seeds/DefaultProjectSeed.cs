using Domain.TaskModelAggregate;
using Domain.TaskModelAggregate.Enumerations;
using Domain.TaskModelAggregate.Ids;
using Domain.TaskModelAggregate.ValueObjects;
using Persistence.DbContexts;
using Persistence.Seeds.Interfaces;

namespace Persistence.Seeds;

public sealed class DefaultProjectSeed(TaskManagerDbContext dbContext) : ISeeder
{
    public readonly TaskManagerDbContext _dbContext = dbContext;

    private const int RANDOM_SEED = 100;
    private const int NUMBER_OF_TASKS = 50;

    private const int MIN_DEADLINE_DAY = 1;
    private const int MAX_DEADLINE_DAYS = 10;

    public void Seed()
    {
        if (_dbContext.TaskModels.Any())
            return;

        List<TaskModel> taskModels = [];
        var random = new Random(RANDOM_SEED);

        for (int i = 1; i <= NUMBER_OF_TASKS; i++)
        {
            var taskModelResult = TaskModel.Create(
                new TaskModelId(Guid.NewGuid()),
                TaskModelTitle.Create($"Task {i}").Value,
                TaskModelDescription.Create($"Description for Task {i}").Value,
                TaskModelPriority.FromValue(random.Next(1, 4)),
                TaskModelDeadline.Create(DateTime.UtcNow.AddDays(random.Next(MIN_DEADLINE_DAY, MAX_DEADLINE_DAYS)), DateTime.UtcNow).Value
            );
            if (taskModelResult.IsSuccess)
            {
                taskModels.Add(taskModelResult.Value);
            }
        }

        _dbContext.TaskModels.AddRange(taskModels);
        _dbContext.SaveChanges();
    }
}
