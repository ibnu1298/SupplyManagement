using Microsoft.EntityFrameworkCore;
using SupplyManagement.Core.Repositories.Interfaces;
using SupplyManagement.DataAccess;
using SupplyManagement.DataAccess.Models.Organization;
using SupplyManagement.DataAccess.Models.Security;

namespace SupplyManagement.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<User>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _context.Users
                .Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteRangeAsync(List<User> users)
        {
            _context.Users.RemoveRange(users);
        }
    }
}
