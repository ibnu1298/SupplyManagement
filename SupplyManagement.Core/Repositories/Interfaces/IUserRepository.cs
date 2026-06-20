using SupplyManagement.DataAccess.Models.Security;

namespace SupplyManagement.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<List<User>> GetByCompanyIdAsync(Guid companyId);
        Task DeleteRangeAsync(List<User> users);
    }
}
