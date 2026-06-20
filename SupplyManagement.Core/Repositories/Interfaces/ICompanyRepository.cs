using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.DataAccess.Models.Organization;
using SupplyManagement.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByIdAsync(Guid id);

        Task<List<Company>> GetAllAsync();

        Task AddAsync(Company company);

        Task<bool> ExistsByEmailAsync(string email);

        Task<List<Company>> GetByStatusAsync(CompanyStatus[] statuses);

        Task DeleteAsync(Company company);

    }
}
