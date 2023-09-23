using lw.Core.Cte;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configureServices(builder.Services);
configureApp(builder.Build());
void configureServices(IServiceCollection services)
{
	services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    var databaseSettings = configuration.GetSection(ConfigKeys.DatabaseSettings).Get<DatabaseSettings>();

    services.AddDatabase(databaseSettings);
	services.RegisterServices();
	services.RegisterWebServices();

	services.AddControllersWithViews();

    services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(Assembly.Load("lw.Api"))); ;
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    #region Configure Swagger  
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Liteweb CMS Core", Version = "v1" });
        c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "basic",
            In = ParameterLocation.Header,
            Description = "Basic Authorization header using the Bearer scheme."
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });
    });
    #endregion

    services.AddAuthentication("BasicAuthentication")
        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

}
void configureApp(WebApplication app)
{
	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
	
    app.UseAuthentication();
    app.UseRouting();

    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        // Configure your endpoints here
        endpoints.MapControllers();
    });
   

    app.UseStaticFiles();
    


    app.MapControllerRoute(
		name: "default",
		pattern: "{PageUrl?}/{SubPageUrl?}/{SubSubPageUrl?}",
        defaults: new { controller = "WebPage", action = "Index" });
    

    app.Run();
}