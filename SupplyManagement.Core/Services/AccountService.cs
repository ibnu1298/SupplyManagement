using SupplyManagement.Core.Dtos;
using SupplyManagement.Core.Repositories.Interfaces;
using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.Core.Services.Interfaces;
using SupplyManagement.DataAccess.Models.Organization;
using SupplyManagement.DataAccess.Models.Security;
using SupplyManagement.Shared.Enums;
using SupplyManagement.Shared.Objects.Dtos;
using System.Net;

namespace SupplyManagement.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IRoleRepository _roleRepository;

        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IRoleRepository roleRepository,
            IPasswordService passwordService,
            IUnitOfWork unitOfWork,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _roleRepository = roleRepository;
            _passwordService = passwordService;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }
        public async Task<ObjectDto<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                return new("The username or password you entered is incorrect.", HttpStatusCode.Unauthorized);

            if (!_passwordService.VerifyPassword(user, user.Password, request.Password))
            {
                return new("The username or password you entered is incorrect.", HttpStatusCode.Unauthorized);
            }

            var role = await _roleRepository.GetByIdAsync(user.RoleId);

            if (role == null)
            {
                return new("Role not found", HttpStatusCode.NotFound);
            }

            var token = _jwtService.GenerateToken(user, role);

            var loginResult =  new LoginResponseDto
            {
                Id = user.Id,
                Name = user.FullName,
                Email = user.Email,
                Token = token
            };

            return new(httpStatusCode: HttpStatusCode.OK)
            {
                Obj = loginResult
            };
        }
        public async Task<BaseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser =
                await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
                return new("Email already registered.", HttpStatusCode.BadRequest);

            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = request.CompanyName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Status = CompanyStatus.PendingAdminApproval
            };

            await _companyRepository.AddAsync(company);

            var VendorRole = await _roleRepository.GetByCodeAsync("VENDOR");

            if (VendorRole == null)
            {
                return new("Role Not Found", HttpStatusCode.BadRequest);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                CompanyId = company.Id,
                Username = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                IsActive = false,
                RoleId = VendorRole.Id,
            };

            var hashedPassword =_passwordService.HashPassword(user, request.Password);
            user.SetPassword(hashedPassword);

            await _userRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();

            return new("Registration submitted successfully.", HttpStatusCode.OK);
        }
    }
}
