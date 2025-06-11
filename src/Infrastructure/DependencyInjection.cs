using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;

namespace Infrastructure;

/// <summary>
/// Registers infrastructure-level services: EF Core DbContext, repositories, external providers (Azure Storage, ServiceBus, etc.).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Connection string defined in appsettings.json ⇒ "ConnectionStrings:Default"
        var connectionString = configuration.GetConnectionString("Default")
                              ?? throw new InvalidOperationException("Connection string 'Default' not found.");

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(connectionString));

        // Repositories & UoW
        services.AddScoped<Domain.Abstractions.Repositories.ITodoRepository, Repositories.TodoRepository>();
        services.AddScoped<Domain.Abstractions.IUnitOfWork, Persistence.UnitOfWork>();

        // TODO: register Azure Storage, ServiceBus, etc.
        return services;
    }
}
