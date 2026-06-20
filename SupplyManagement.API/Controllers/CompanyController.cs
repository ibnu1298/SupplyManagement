using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.Core.Services.Interfaces;
using SupplyManagement.Shared.Objects.Dtos;

namespace SupplyManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("{companyId:Guid}")]
        public async Task<ObjectDto<CompanyApprovalDto>> GetByIdAsync([FromRoute]Guid companyId) => await _companyService.GetByIdAsync(companyId);

        [HttpPut("{companyId:Guid}")]
        public async Task<BaseDto> Update([FromRoute]Guid companyId, [FromBody] UpdateCompanyRequestDto request) => await _companyService.UpdateAsync(companyId, request);
        
        [HttpDelete("{companyId:Guid}")]
        public async Task<BaseDto> Delete([FromRoute]Guid companyId) => await _companyService.DeleteCompanyAsync(companyId);

        [Authorize(Roles = "ADMIN,MANAGER")]
        [HttpGet("pending-approval")]
        public async Task<ObjectDto<List<CompanyApprovalDto>>> GetPendingApproval() => await _companyService.GetPendingApprovalAsync();

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{companyId}/admin-approval")]
        public async Task<BaseDto> AdminApproval([FromRoute] Guid companyId, ApprovalRequestDto request) => await _companyService.AdminApprovalAsync(companyId, request);

        [Authorize(Roles = "MANAGER")]
        [HttpPut("{companyId}/manager-approval")]
        public async Task<BaseDto> ManagerApproval([FromRoute] Guid companyId, ApprovalRequestDto request) => await _companyService.ManagerApprovalAsync(companyId, request);


    }
}
