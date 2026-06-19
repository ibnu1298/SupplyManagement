using SupplyManagement.Core.Repositories;
using SupplyManagement.Core.Repositories.Interfaces;
using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.Core.Services.Interfaces;
using SupplyManagement.DataAccess.Repositories;
using SupplyManagement.Shared.Enums;
using SupplyManagement.Shared.Objects.Dtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SupplyManagement.Core.Services
{
    public class CompanyService : ICompanyService
    {

        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CompanyService(
            ICompanyRepository companyRepository,
            IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseDto> AdminApprovalAsync(Guid companyId, ApprovalRequestDto request)
        {
            var company =  await _companyRepository.GetByIdAsync(companyId);

            if (company == null)
                return new("Company not found", HttpStatusCode.NotFound);

            if (company.Status != CompanyStatus.PendingAdminApproval)
                return new("Company is not awaiting admin approval", HttpStatusCode.BadRequest);

            company.Status = request.IsApproved ? CompanyStatus.PendingManagerApproval : CompanyStatus.Rejected;

            company.Remarks = request.Remarks ?? string.Empty;

            await _unitOfWork.SaveChangesAsync();

            return new(
                request.IsApproved
                    ? "Admin approval successful"
                    : "Company rejected",
                HttpStatusCode.OK);
        }

        public async Task<ObjectDto<List<CompanyApprovalDto>>> GetPendingApprovalAsync()
        {
            var companies = await _companyRepository.GetByStatusAsync(CompanyStatus.PendingAdminApproval);

            var result = companies.Select(x => new CompanyApprovalDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Status = x.Status
            }).ToList();

            return new(httpStatusCode: HttpStatusCode.OK)
            {
                Obj = result
            };
        }

        public async Task<BaseDto> ManagerApprovalAsync(Guid companyId, ApprovalRequestDto request)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);

            if (company == null)
                return new("Company not found", HttpStatusCode.NotFound);

            if (company.Status != CompanyStatus.PendingManagerApproval)
                return new("Company is not awaiting manager logistic approval", HttpStatusCode.BadRequest);

            company.Status = request.IsApproved ? CompanyStatus.Active : CompanyStatus.Rejected;

            company.Remarks = request.Remarks ?? string.Empty;

            await _unitOfWork.SaveChangesAsync();

            return new(request.IsApproved ? "Manager logistic approval successful" : "Company rejected", HttpStatusCode.OK);
        }

        public async Task<BaseDto> UpdateAsync(Guid companyId, UpdateCompanyRequestDto request)
        {

            var company = await _companyRepository.GetByIdAsync(companyId);

            if (company == null)
            {
                return new("Company not found", HttpStatusCode.NotFound);
            }

            if (company.Status != Shared.Enums.CompanyStatus.Active)
            {
                return new("Cannot update Company", HttpStatusCode.Unauthorized);
            }

            company.Name = request.Name;
            company.Email = request.Email;
            company.PhoneNumber = request.PhoneNumber;
            company.BusinessField = request.BusinessField ?? string.Empty;
            company.CompanyType = request.CompanyType ?? string.Empty;

            await _unitOfWork.SaveChangesAsync();

            return new(
                "Company updated successfully",
                HttpStatusCode.OK);
        }
    }
}
