using Microsoft.AspNetCore.Mvc;
using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.Shared.Enums;
using SupplyManagement.Shared.Objects.Dtos;
using SupplyManagement.Web.Filters;
using SupplyManagement.Web.Helper;
using SupplyManagement.Web.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SupplyManagement.Web.Controllers
{
    [JwtRequired]
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public HomeController(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["ApiSettings:BaseUrl"] ?? string.Empty;
        }
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("token");

            var companyId = JwtHelper.GetCompanyId(token ?? string.Empty);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"{_baseUrl}/api/company/{companyId}");

            if (!response.IsSuccessStatusCode)
            {
                return View();
            }

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<CompanyModel>>(
                responseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
            var company = result?.Obj;

            return View(company);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
