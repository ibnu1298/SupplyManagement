using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.Shared.Objects.Dtos;

namespace SupplyManagement.Core.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<ObjectDto<CompanyApprovalDto>> GetByIdAsync(Guid companyId);
        Task<BaseDto> UpdateAsync(Guid companyId, UpdateCompanyRequestDto request);
        Task<ObjectDto<List<CompanyApprovalDto>>> GetPendingApprovalAsync();
        Task<BaseDto> AdminApprovalAsync(Guid companyId, ApprovalRequestDto request);
        Task<BaseDto> ManagerApprovalAsync(Guid companyId, ApprovalRequestDto request);
    }
}
