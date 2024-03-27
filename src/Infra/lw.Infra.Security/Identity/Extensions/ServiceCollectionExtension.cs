using lw.Core.Data;
using lw.Infra.DataContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace lw.Infra.Security.Identity;
public static class ServiceCollectionExtension
{
	public static IServiceCollection AddIdentity(this IServiceCollection services)
	{
		

		services.AddIdentityCore<User>(options =>
		{
			options.User.RequireUniqueEmail = true;
			options.Password.RequireUppercase = false;
			options.Password.RequireNonAlphanumeric = false;
		})
		.AddRoles<IdentityRole>() // Add this line if you're using roles
		.AddSignInManager<SignInManager<User>>() // This will add the SignInManager
		.AddEntityFrameworkStores<AppDbContext>()
		.AddDefaultTokenProviders();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.AccessDeniedPath = "/Admin/accessdenied";
                options.LoginPath = "/Admin/login";
            });
        //.AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        //.AddApiEndpoints();
        //services.ConfigureApplicationCookie(options =>
        //{
        //	options.AccessDeniedPath = "/Admin/accessdenied";
        //	options.LoginPath = "/Admin/login";
        //});
        return services;
	}
}