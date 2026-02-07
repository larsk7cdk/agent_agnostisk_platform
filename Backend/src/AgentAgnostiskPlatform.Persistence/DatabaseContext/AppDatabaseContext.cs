using Microsoft.EntityFrameworkCore;
using AgentAgnostiskPlatform.Domain.Entities;

namespace AgentAgnostiskPlatform.Persistence.DatabaseContext;

public sealed class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
    // DbSets for entities


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Loads all configurations that implement IEntityTypeConfiguration<T> from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    // Add DateTime for add and modify entities
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}