using Microsoft.Extensions.DependencyInjection;
using SupplyManagement.Core.Repositories;
using SupplyManagement.Core.Repositories.Interfaces;
using SupplyManagement.Core.Services;
using SupplyManagement.Core.Services.Interfaces;
using SupplyManagement.DataAccess.Repositories;
namespace SupplyManagement.Core
{
    /// <summary>
    /// Contains extension methods for registering core services and middlewares
    /// into the dependency injection container.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the core services, middlewares, and AutoMapper profiles
        /// into the provided <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add registrations to.</param>
        /// <returns>The <see cref="IServiceCollection"/> with added services.</returns>
        public static IServiceCollection RegisterCore(this IServiceCollection services)
        {
            #region Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Service
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IAccountService, AccountService>();
            #endregion

            return services;
        }
    }
}
