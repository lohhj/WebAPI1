using Microsoft.Extensions.DependencyInjection;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IFreelancerRepository>(provider => new DapperFreelancerRepository(connectionString));
        return services;
    }
}
