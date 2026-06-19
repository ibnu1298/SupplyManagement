using SupplyManagement.DataAccess.Models.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(Guid id);

        Task<Role?> GetByCodeAsync(string code);

        Task<List<Role>> GetAllAsync();

        Task AddAsync(Role role);
    }
}
