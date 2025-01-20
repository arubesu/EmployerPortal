using EmployerPortal.Data;
using EmployerPortal.Utils;

namespace EmployerPortal.Extensions;

public static class DatabaseExtensions
{
    public static void SeedDatabase(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EmployerPortalDbContext>();
        context.Database.EnsureCreated();
        AppSeed.Seed(context);
    }
}
