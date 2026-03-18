using Persistence.DbContexts;
using Persistence.Seeds.Interfaces;

namespace Persistence.Seeds;

internal class DefaultProjectSeed(TaskManagerDbContext dbContext) : ISeeder
{
    public readonly TaskManagerDbContext _dbContext = dbContext;

    public void Seed()
    {

    }
}
