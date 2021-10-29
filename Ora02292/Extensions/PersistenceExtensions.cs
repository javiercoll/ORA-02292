using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace Ora02292.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var dbProvider = configuration.GetValue<string>("Database:Provider");

            switch (dbProvider)
            {
                case Provider.Oracle:
                    services.AddDbContext<Ora02292DbContext>(options =>
                        options.UseOracle(configuration.GetConnectionString(dbProvider),
                            x => x.MigrationsAssembly(dbProvider)));
                    break;
                case Provider.Sqlite:
                    services.AddDbContext<Ora02292DbContext>(options =>
                        options.UseSqlite(configuration.GetConnectionString(dbProvider),
                            x => x.MigrationsAssembly(dbProvider)));
                    break;
            }

            return services;
        }
        
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Ora02292DbContext>();
            context.Database.Migrate();
            context.Database.EnsureCreated();
            return host;
        }
    }
}