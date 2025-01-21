using EmployerPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployerPortal.Data;

public class EmployerPortalDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public EmployerPortalDbContext(DbContextOptions<EmployerPortalDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}
