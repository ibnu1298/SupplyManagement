using Microsoft.EntityFrameworkCore;
using SupplyManagement.Core.Repositories.Interfaces;
using SupplyManagement.DataAccess.Models.Security;

namespace SupplyManagement.DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByIdAsync(Guid id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Role?> GetByCodeAsync(string code)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Roles
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }
    }
}