using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure.Persistence;

// TODO: Replace placeholder DbSets with real aggregates (e.g., Todos)
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Example entity
    public DbSet<Domain.Entities.Todo> Todos => Set<Domain.Entities.Todo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
