using Microsoft.EntityFrameworkCore;
using Polly;

namespace Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder MigrateDbContext<TDbContext>(this IApplicationBuilder builder)
        where TDbContext : DbContext
    {
        using var scope = builder.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        Policy
            .Handle<Exception>()
            .WaitAndRetry(
            retryCount: 3,
            _ => TimeSpan.FromSeconds(15))
            .Execute(dbContext.Database.Migrate);

        return builder;
    }
}