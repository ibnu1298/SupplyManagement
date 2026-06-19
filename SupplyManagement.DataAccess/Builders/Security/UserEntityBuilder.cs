using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyManagement.DataAccess.Bases;
using SupplyManagement.DataAccess.Models.Security;

namespace SupplyManagement.DataAccess.Builders.Security
{
    internal class UserEntityBuilder : EntityBaseBuilder<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder
                .Property(e => e.Username)
                .HasMaxLength(50);

            builder
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasMaxLength(50);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}
