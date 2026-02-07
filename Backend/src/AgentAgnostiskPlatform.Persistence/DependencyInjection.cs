using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AgentAgnostiskPlatform.Application.Contracts.Interfaces.Persistence;
using AgentAgnostiskPlatform.Persistence.DatabaseContext;
using AgentAgnostiskPlatform.Persistence.Repositories;
using AgentAgnostiskPlatform.Persistence.Repositories.Shared;

namespace AgentAgnostiskPlatform.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("appdb"));
        });

        // Service registrations
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}