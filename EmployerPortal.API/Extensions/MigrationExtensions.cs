using EmployerPortal.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployerPortal.Extensions;

public static class MigrationExtensions
{
    public static void MigrateDatabase(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EmployerPortalDbContext>();
        context.Database.Migrate();
    }
}