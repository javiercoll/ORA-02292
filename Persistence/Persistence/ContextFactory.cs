using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public class ContextFactory : IDesignTimeDbContextFactory<Ora02292DbContext>
    {
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public Ora02292DbContext CreateDbContext(string[] args)
        {
            var providerType = Provider.Sqlite;
            var providerIndex = Array.IndexOf(args, "--provider");
            if (providerIndex != -1 && args.Length > providerIndex + 1)
            {
                providerType = args[providerIndex + 1];
            }

            var configuration = GetConfiguration();
            var connectionString = GetConnectionString(configuration, providerType);

            var optionsBuilder = new DbContextOptionsBuilder<Ora02292DbContext>();

            switch (providerType)
            {
                case Provider.Sqlite:
                    optionsBuilder.UseSqlite(connectionString, b => b.MigrationsAssembly(providerType));
                    break;
                case Provider.Oracle:
                    optionsBuilder.UseOracle(connectionString, b => b.MigrationsAssembly(providerType));
                    break;
                default:
                    throw new ArgumentException($"Unknown provider: {providerType}");
            }
            
            return new Ora02292DbContext(optionsBuilder.Options);
        }

        private static IConfiguration GetConfiguration()
        {
            var basePath = Directory.GetCurrentDirectory();

            var environmentName = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);

            return new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", true)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static string GetConnectionString(IConfiguration configuration, string provider)
        {
            var connectionString = configuration.GetConnectionString(provider);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{provider}' is null or empty.",
                    nameof(connectionString));
            }

            return connectionString;
        }
    }
}