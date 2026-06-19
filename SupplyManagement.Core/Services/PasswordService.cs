using Microsoft.AspNetCore.Identity;
using SupplyManagement.Core.Services.Interfaces;
using SupplyManagement.DataAccess.Models.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string hashedPassword, string password)
        {
            var result = _hasher.VerifyHashedPassword(
                user,
                hashedPassword,
                password);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
