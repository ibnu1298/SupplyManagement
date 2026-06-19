using SupplyManagement.Core.Dtos;
using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.Shared.Objects.Dtos;

namespace SupplyManagement.Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ObjectDto<LoginResponseDto>> LoginAsync(LoginRequestDto request);
        Task<BaseDto> RegisterAsync(RegisterRequestDto request);
    }
}
