using Microsoft.EntityFrameworkCore;
using SupplyManagement.Core.Repositories.Interfaces;
using SupplyManagement.DataAccess;
using SupplyManagement.DataAccess.Models.Organization;
using SupplyManagement.Shared.Enums;

namespace SupplyManagement.Core.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Company?> GetByIdAsync(Guid id)
        {
            return await _context.Companies
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _context.Companies
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Companies
                .AnyAsync(x => x.Email == email);
        }

        public async Task<List<Company>> GetByStatusAsync(CompanyStatus status)
        {
            return await _context.Companies
                .Where(x => x.Status == status)
                .ToListAsync();
        }
    }
}
