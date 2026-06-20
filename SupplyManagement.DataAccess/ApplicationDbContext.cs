using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SupplyManagement.DataAccess.Bases;
using SupplyManagement.DataAccess.Builders.Organization;
using SupplyManagement.DataAccess.Builders.Security;
using SupplyManagement.DataAccess.Models.Organization;
using SupplyManagement.DataAccess.Models.Security;
using SupplyManagement.Shared.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SupplyManagement.DataAccess
{
    /// <summary>
    /// The main database context for the application.
    /// <para>
    /// Inherits from <see cref="DbContextBase"/> which automatically manages auditing fields like Created/Modified.
    /// </para>
    /// <para>
    /// Contains <see cref="DbSet{T}"/> properties for system, security, and organization entities.
    /// Configures entity mappings via dedicated entity builders.
    /// Provides a method to seed initial example data.
    /// </para>
    /// </summary>
    public class ApplicationDbContext(DbContextOptions _options, CurrentUserAccessor _currentUserAccessor)
        : DbContextBase(_options, _currentUserAccessor)
    {

        private readonly PasswordHasher<User> _hasher = new();
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Configures entity mappings using their respective builders.
        /// </summary>
        /// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Organization
            new CompanyEntityBuilder().Configure(modelBuilder.Entity<Company>());
            #endregion

            #region Security
            new RoleEntityBuilder().Configure(modelBuilder.Entity<Role>());
            new UserEntityBuilder().Configure(modelBuilder.Entity<User>());
            #endregion
        }

        /// <summary>
        /// Seeds initial data for example companies and admin user if they don't exist.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task SeedDataUserAdminAsync()
        {
            if (await Users.AnyAsync()) return;

            var adminRoleId = await Roles .Where(x => x.Code == "ADMIN") .Select(x => x.Id).FirstOrDefaultAsync();
            var logisticRoleId = await Roles.Where(x => x.Code == "MANAGER").Select(x => x.Id).FirstOrDefaultAsync();
            var vendorRoleId = await Roles.Where(x => x.Code == "VENDOR").Select(x => x.Id).FirstOrDefaultAsync();

            var users = new List<User>
            {
                new()
                {
                    Id = new Guid("1604ea12-71bf-4be5-b027-93b4d4e3e796"),
                    Username = "admin@mail.com",
                    Email = "admin@mail.com",
                    FullName = "System Admin",
                    IsActive = true,
                    CompanyId = new Guid("183a808d-1a0d-4bf7-afaa-b2066cd77170"),
                    RoleId = adminRoleId,
                    CreatedBy = "System"
                },
                new()
                {
                    Id = new Guid("2604ea12-71bf-4be5-b027-93b4d4e3e796"),
                    Username = "manager@mail.com",
                    Email = "manager@mail.com",
                    FullName = "Logistic Manager",
                    IsActive = true,
                    CompanyId = new Guid("183a808d-1a0d-4bf7-afaa-b2066fd77180"),
                    RoleId = logisticRoleId,
                    CreatedBy = "System"
                },
                new()
                {
                    Id = new Guid("3604ea12-71bf-4be5-b027-93b4d4e3e796"),
                    Username = "vendor@mail.com",
                    Email = "vendor@mail.com",
                    FullName = "Vendor User",
                    IsActive = true,
                    CompanyId = new Guid("183a808d-1a0d-4bf7-afaa-b2066fd77170"),
                    RoleId = vendorRoleId,
                    CreatedBy = "System"
                }
            };

            foreach (var user in users)
            {
                var hashedPassword =
                    _hasher.HashPassword(user, "123");

                user.SetPassword(hashedPassword);
            }

            await Users.AddRangeAsync(users);
            await SaveChangesAsync();
        }

    }
}
