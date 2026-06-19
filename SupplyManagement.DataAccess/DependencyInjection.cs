using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;
using SupplyManagement.Shared.Objects.Configs;


namespace SupplyManagement.DataAccess
{
    /// <summary>
    /// Provides extension methods to register DataAccess services into the dependency injection container.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the database context <see cref="ApplicationDbContext"/>.
        /// </summary>
        public static IServiceCollection RegisterDataAccess(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var databaseConfig = configuration
                .GetSection(nameof(DatabaseConfig))
                .Get<DatabaseConfig>() ?? new();

            var executingAssemblyName = Assembly
                .GetExecutingAssembly()
                .GetName()
                .Name;

            if (!string.IsNullOrWhiteSpace(databaseConfig.ConnectionString))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(
                        databaseConfig.ConnectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(executingAssemblyName);
                            sqlOptions.CommandTimeout(databaseConfig.CommandTimeout);
                        });
                });

                Log.Logger.Information("Database registered using SQL Server.");
            }
            else
            {
                Log.Logger.Error("No Database connection string found in configuration.");
            }

            return services;
        }
    }
}