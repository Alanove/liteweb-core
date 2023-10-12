namespace lw.Infra.DataContext;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, 
        DatabaseSettings? databaseSettings)
    {
        if (databaseSettings != null)
        {
            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseLazyLoadingProxies();
                switch (databaseSettings.Provider)
                {
                    case DatabaseProviders.MsSql:
                        o.UseSqlServer(databaseSettings.ConnectionString);
                        break;
                    case DatabaseProviders.Postgresql:
                        o.UseNpgsql(databaseSettings.ConnectionString);
                        break;
                    case DatabaseProviders.SqlLite:
                        o.UseSqlite(databaseSettings.ConnectionString);
                        break;
                    default:
                        throw new Exception("No Sql providers provided");
                }
            });
        }
        else
        {
            throw new Exception("Please add DbSettings in appsettings.json");
        }
        return services;
    }
}
