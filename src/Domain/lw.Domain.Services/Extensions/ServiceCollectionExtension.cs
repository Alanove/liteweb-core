using Microsoft.Extensions.DependencyInjection;

namespace lw.Domain.Services;
public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
		services.AddScoped<IPagesService, PagesService>();
		services.AddScoped<ITagsService, TagsService>();
        services.AddScoped<IWebsiteService, WebsiteService>();
        services.AddScoped<IMenuService, MenuService>();
        return services;
    }
}