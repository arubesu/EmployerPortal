using EmployerPortal.Data;
using EmployerPortal.Models;

namespace EmployerPortal.Utils;

public static class AppSeed
{
    public static void Seed(EmployerPortalDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                    new User { Username = "admin" },
                    new User { Username = "developer" },
                    new User { Username = "manager" }
                );

            context.SaveChanges();
        }
    }
}
