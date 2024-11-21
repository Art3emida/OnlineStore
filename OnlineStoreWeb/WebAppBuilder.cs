namespace OnlineStoreWeb;

using OnlineStore.Infrastructure.Persistence.Initializers;
using OnlineStoreWeb.Configuration;
using OnlineStoreWeb.Middleware;

public class WebAppBuilder
{
    private readonly WebApplicationBuilder _builder;

    public WebAppBuilder(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);
    }

    public void Run()
    {
        DependencyInjectionConfig.RegisterServices(
            _builder.Services,
            _builder.Configuration
        );

        _builder.Logging.ClearProviders();
        _builder.Logging.AddConsole();
        _builder.Logging.AddFile("Logs/app-{Date}.txt");
        
        WebApplication app = _builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            SeedDatabase(app);
        }

        InitMiddleware(app);

        app.MapControllerRoute(
            name: "default",
            pattern: "{area=common}/{controller=Site}/{action=index}/{id?}"
        );
        app.Run();
    }

    private void InitMiddleware(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/error/500");
            app.UseHsts();
        }

        app.UseSession();
        app.UseStatusCodePagesWithReExecute("/error/{0}");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<CartMiddleware>();
    }

    private void SeedDatabase(WebApplication app)
    {
        using (IServiceScope scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            dbInitializer.InitializeAsync().GetAwaiter().GetResult();
        }
    }
}
