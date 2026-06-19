using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class adddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "BusinessField", "CompanyType", "Created", "CreatedBy", "Email", "Modified", "ModifiedBy", "Name", "PhoneNumber", "Remarks", "RowStatus", "Status", "Website" },
                values: new object[] { new Guid("183a808d-1a0d-4bf7-afaa-b2066fd77170"), "", "", "", new DateTime(2026, 6, 20, 9, 30, 0, 0, DateTimeKind.Utc), "System", "", null, null, "Company Need Approval", "", "", 0, 1, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("183a808d-1a0d-4bf7-afaa-b2066fd77170"));
        }
    }
}
