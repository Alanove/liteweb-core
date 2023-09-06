using Microsoft.Extensions.DependencyInjection;

namespace lw.Domain.Web;
public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterWebServices(this IServiceCollection services)
    {
        services.AddScoped<IWebPageService, WebPageService>();
        return services;
    }
}