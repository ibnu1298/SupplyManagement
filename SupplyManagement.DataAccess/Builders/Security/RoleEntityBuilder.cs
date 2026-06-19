using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyManagement.DataAccess.Bases;
using SupplyManagement.DataAccess.Models.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.DataAccess.Builders.Security
{
    internal class RoleEntityBuilder : EntityBaseBuilder<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder
                .Property(e => e.Code)
                .HasMaxLength(50);

            builder
                .Property(e => e.Name)
                .HasMaxLength(50);

            builder.HasIndex(x => x.Code)
                 .IsUnique();

            SeedingData_20260520_0930(builder);
        }

        private static void SeedingData_20260520_0930(EntityTypeBuilder<Role> builder)
        {
            var roles = new Role[]
            {
                new() { Id = new Guid("9ffa21e8-f911-48ce-9c68-0ec6ba882302"), Code = "ADMIN", Name = "Administrator" },
                new() { Id = new Guid("9ffa21e8-f911-48ce-9c68-0ec6bc882302"), Code = "MANAGER", Name = "Logistic Manager" },
                new() { Id = new Guid("9ffa21e8-f911-48ce-9c68-0ec6ba812302"), Code = "VENDOR", Name = "Vendor" }
            };

            foreach (var role in roles)
            {
                role.Created = new(2026, 6, 20, 9, 30, 0, DateTimeKind.Utc);
                role.CreatedBy = "System";
            }

            builder.HasData(roles);
        }
    }
}
