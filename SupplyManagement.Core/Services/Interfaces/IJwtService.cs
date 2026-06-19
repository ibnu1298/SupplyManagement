using SupplyManagement.DataAccess.Models.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user, Role role);
    }
}
