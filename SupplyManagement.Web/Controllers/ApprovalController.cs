using Microsoft.AspNetCore.Mvc;
using SupplyManagement.Core.Services.Dtos;
using SupplyManagement.Shared.Objects.Dtos;
using SupplyManagement.Web.Filters;
using SupplyManagement.Web.Helper;
using SupplyManagement.Web.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SupplyManagement.Web.Controllers
{


    [JwtRequired]
    public class ApprovalController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public ApprovalController(IHttpClientFactory factory, IConfiguration config)
        {
            _httpClient = factory.CreateClient();
            _baseUrl = config["ApiSettings:BaseUrl"] ?? string.Empty;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(
                $"{_baseUrl}/api/Company/pending-approval"
            );

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<List<CompanyApprovalDto>>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            var data = result?.Obj ?? new List<CompanyApprovalDto>();

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(Guid id, bool isApproved, string remarks)
        {
            var token = HttpContext.Session.GetString("token");
            var role = JwtHelper.GetRole(token);

            var endpoint = role == "ADMIN"
                ? "admin-approval"
                : "manager-approval";

            var url = $"{_baseUrl}/api/Company/{id}/{endpoint}";

            var payload = new ApprovalRequestDto
            {
                IsApproved = isApproved,
                Remarks = remarks
            };

            var json = JsonSerializer.Serialize(payload);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Approval failed";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var token = HttpContext.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(
                $"{_baseUrl}/api/Company/{id}"
            );

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<CompanyApprovalDto>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            var data = result?.Obj;

            if (data == null)
                return NotFound();

            return View(data);
        }
    }
}
