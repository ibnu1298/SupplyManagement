using SupplyManagement.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Services.Dtos
{
    public class CompanyApprovalDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public CompanyStatus Status { get; set; }
    }
}
