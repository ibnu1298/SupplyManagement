using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyManagement.DataAccess.Bases;
using SupplyManagement.DataAccess.Models.Organization;

namespace SupplyManagement.DataAccess.Builders.Organization
{

    internal class CompanyEntityBuilder : EntityBaseBuilder<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);


            builder
                .Property(e => e.Name)
                .HasMaxLength(50);

            builder
                .Property(e => e.Address)
                .HasMaxLength(150);

            builder
                .Property(e => e.PhoneNumber)
                .HasMaxLength(15);

            builder
                .Property(e => e.Email)
                .HasMaxLength(50);

            builder
                .Property(e => e.Website)
                .HasMaxLength(50);

            builder
                .Property(e => e.Remarks)
                .HasMaxLength(150);

            builder.Property(x => x.Status)
                .IsRequired();

            builder
                .Property(e => e.BusinessField)
                .HasMaxLength(150);

            builder
                .Property(e => e.CompanyType)
                .HasMaxLength(150);


            SeedingData_20260520_0930(builder);
        }

        private static void SeedingData_20260520_0930(EntityTypeBuilder<Company> builder)
        {
            var companies = new Company[]
            {
                new() { Id = new Guid("183a808d-1a0d-4bf7-afaa-b2066cd77170"), Name = "Company1", Status = Shared.Enums.CompanyStatus.Active},
                new() { Id = new Guid("183a808d-1a0d-4bf7-afaa-b2066fd77170"), Name = "Company Need Approval", Status = Shared.Enums.CompanyStatus.PendingAdminApproval},
                new() { Id = new Guid("183a808d-1a0d-4bf7-afaa-b2066fd77180"), Name = "Company Need Approval", Status = Shared.Enums.CompanyStatus.PendingAdminApproval}
            };

            foreach (var company in companies)
            {
                company.Created = new DateTime(2026, 6, 20, 9, 30, 0, DateTimeKind.Utc);
                company.CreatedBy = "System";
            }

            builder.HasData(companies);
        }
    }
}
