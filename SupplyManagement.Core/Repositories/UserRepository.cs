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

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}
