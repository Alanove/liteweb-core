using lw.Admin.Services;
using Microsoft.Extensions.DependencyInjection;

namespace lw.Admin;
public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterAdminServices(this IServiceCollection services)
    {
        services.AddScoped<IAdminPagesService, AdminPagesService>();
        return services;
    }
}