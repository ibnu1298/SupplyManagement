using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Dtos
{
    public class LoginResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
