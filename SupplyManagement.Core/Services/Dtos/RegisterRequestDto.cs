using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Services.Dtos
{
    public class RegisterRequestDto
    {
        public string CompanyName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
