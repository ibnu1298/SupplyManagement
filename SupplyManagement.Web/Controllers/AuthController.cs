using Microsoft.AspNetCore.Mvc;
using SupplyManagement.Web.Filters;
using SupplyManagement.Web.Models;
using System.Text;
using System.Text.Json;

namespace SupplyManagement.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? string.Empty;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var payload = new
            {
                email = email,
                password = password
            };

            var json = JsonSerializer.Serialize(payload);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{_baseUrl}/api/account/login",
                content
            );

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Login gagal";
                return View();
            }

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<LoginDto>>(
                responseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            HttpContext.Session.SetString("token", result.Obj.Token);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{_baseUrl}/api/account/register",
                content
            );

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Register failed";
                return View(model);
            }

            return RedirectToAction("Login");
        }
    }
}
