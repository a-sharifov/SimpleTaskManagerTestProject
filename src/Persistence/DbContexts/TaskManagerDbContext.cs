using Domain.TaskModelAggregate;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts;

public class TaskManagerDbContext : DbContext
{
    public DbSet<TaskModel> TaskModels { get; set; }

    public TaskManagerDbContext()
    {
    }

    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Persistence.AssemblyReference.Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();        
}
