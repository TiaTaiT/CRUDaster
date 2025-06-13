using CRUDaster.Core.Application.Interfaces;
using CRUDaster.ExternalServices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUDaster.ExternalServices;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserContextService, UserContextService>();
        
        return services;
    }
}
