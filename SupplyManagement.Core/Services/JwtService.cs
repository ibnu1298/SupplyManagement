using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SupplyManagement.Core.Services.Interfaces;
using SupplyManagement.DataAccess.Models.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SupplyManagement.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user, Role role)
        {
            var key = _configuration["Jwt:Key"]!;

            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, role.Code),

            new("UserId", user.Id.ToString()),
            new("CompanyId", user.CompanyId.ToString()),
            new("RoleId", role.Id.ToString())
        };

            var securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key));

            var credentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256);

            var expireMinutes =
                int.Parse(_configuration["Jwt:ExpireMinutes"]!);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
