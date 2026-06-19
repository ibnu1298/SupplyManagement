using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Services.Dtos
{
    public class UpdateCompanyRequestDto
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string? BusinessField { get; set; }

        public string? CompanyType { get; set; }

        public string? PhotoUrl { get; set; }
    }
}
