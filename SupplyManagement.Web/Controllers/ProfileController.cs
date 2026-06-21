using Microsoft.AspNetCore.Mvc;
using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.DataAccess.Models.Organization;
using SupplyManagement.Web.Filters;
using SupplyManagement.Web.Helper;
using SupplyManagement.Web.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SupplyManagement.Web.Controllers
{
    [JwtRequired]
    public class ProfileController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProfileController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? string.Empty;
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

        [HttpPost]
        public async Task<IActionResult> UpdateCompany(CompanyModel request)
        {

            var token = HttpContext.Session.GetString("token");
            var companyId = JwtHelper.GetCompanyId(token??string.Empty);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            

            var response = await _httpClient.PutAsJsonAsync(
                $"{_baseUrl}/api/company/{companyId}",
                request);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to update company.";
                return View("Index", request);
            }

            TempData["Success"] = "Company updated successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
