using SupplyManagement.DataAccess.Models.Security;

namespace SupplyManagement.Core.Services.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string password);
    }
}
