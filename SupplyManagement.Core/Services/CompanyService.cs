using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupplyManagement.Core.Repositories;
using SupplyManagement.Core.Repositories.Interfaces;
using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.Core.Services.Interfaces;
using SupplyManagement.DataAccess.Models.Organization;
using SupplyManagement.DataAccess.Models.Security;
using SupplyManagement.DataAccess.Repositories;
using SupplyManagement.Shared.Enums;
using SupplyManagement.Shared.Objects.Dtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SupplyManagement.Core.Services
{
    public class CompanyService(ICompanyRepository _companyRepository, IUserRepository _userRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : ICompanyService
    {

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

        public async Task<ObjectDto<CompanyApprovalDto>> GetByIdAsync(Guid companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
                return new("Role not found", HttpStatusCode.NotFound);

            var companyData = _mapper.Map<CompanyApprovalDto>(company);

            return new(httpStatusCode: HttpStatusCode.OK)
            {
                Obj = companyData
            };
        }

        public async Task<ObjectDto<List<CompanyApprovalDto>>> GetPendingApprovalAsync()
        {
            CompanyStatus[] statuses =
            [
                CompanyStatus.PendingAdminApproval,
                CompanyStatus.PendingManagerApproval
            ];
            var companies = await _companyRepository.GetByStatusAsync(statuses);


            var result = _mapper.Map<List<CompanyApprovalDto>>(companies);

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

            _mapper.Map(request, company);

            await _unitOfWork.SaveChangesAsync();

            return new(
                "Company updated successfully",
                HttpStatusCode.OK);
        }

        public async Task<BaseDto> DeleteCompanyAsync(Guid id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            if (company == null)
            {
                return new("company not found", HttpStatusCode.NotFound);
            }

            // optional: cek user tanpa Include
            var Users = await _userRepository.GetByCompanyIdAsync(id);

            if (Users != null)
            {
                await _userRepository.DeleteRangeAsync(Users);
            }

            await _companyRepository.DeleteAsync(company);

            await _unitOfWork.SaveChangesAsync();

            return new(
               "Company deleted successfully",
               HttpStatusCode.OK);
        }
    }
}
