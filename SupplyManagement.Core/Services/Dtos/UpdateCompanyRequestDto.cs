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
        public string Address { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string CompanyType { get; set; } = string.Empty;
        public string BusinessField { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
