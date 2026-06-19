using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplyManagement.Core.Dtos;
using SupplyManagement.Core.Services;
using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.Core.Services.Interfaces;
using SupplyManagement.Shared.Objects.Dtos;

namespace SupplyManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ObjectDto<LoginResponseDto>> Login(LoginRequestDto request) => await _accountService.LoginAsync(request);

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<BaseDto> Register(RegisterRequestDto request) => await _accountService.RegisterAsync(request);
        
    }
}
