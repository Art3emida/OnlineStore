namespace OnlineStore.Infrastructure.Persistence.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class MasterDbContextFactory : IDesignTimeDbContextFactory<MasterDbContext>
{
    public MasterDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OnlineStoreWeb"))
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MasterDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MasterConnection"));

        return new MasterDbContext(optionsBuilder.Options);
    }
}
