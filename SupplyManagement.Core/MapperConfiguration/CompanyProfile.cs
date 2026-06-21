using AutoMapper;
using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.DataAccess.Models.Organization;

namespace SupplyManagement.Core.MapperConfiguration
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyApprovalDto>();

            CreateMap<UpdateCompanyRequestDto, Company>()
            .ForMember(dest => dest.BusinessField,
                opt => opt.MapFrom(src => src.BusinessField ?? string.Empty))
            .ForMember(dest => dest.CompanyType,
                opt => opt.MapFrom(src => src.CompanyType ?? string.Empty));
        }
    }
}
