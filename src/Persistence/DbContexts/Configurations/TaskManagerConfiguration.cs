using Domain.TaskModelAggregate;
using Domain.TaskModelAggregate.Enumerations;
using Domain.TaskModelAggregate.Ids;
using Domain.TaskModelAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DbContexts.Configurations;

internal sealed class TaskManagerConfiguration : IEntityTypeConfiguration<TaskModel>
{
    public void Configure(EntityTypeBuilder<TaskModel> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(x => x.Id)
           .HasColumnName("id")
           .HasConversion(id => id.Value, v => new TaskModelId(v))
           .ValueGeneratedNever();

        builder.Property(t => t.Title)
            .HasConversion(
                v => v.Value,
                v => TaskModelTitle.Create(v).Value);

        builder.Property(t => t.Description).HasConversion(
                v => v.Value,
                v => TaskModelDescription.Create(v).Value);

        builder.Property(t => t.Deadline).HasConversion(
                v => v.Value,
                v => TaskModelDeadline.Create(v, DateTime.UtcNow).Value);

        builder.Property(t => t.Priority)
            .HasConversion(
                v => v.Value,
                v => TaskModelPriority.FromValue(v))
            .IsRequired();

        builder.Property(t => t.IsCompleted).IsRequired();

        builder.Ignore(t => t.Status);
    }
}
