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
            _baseUrl = config["ApiBaseUrl"] ?? string.Empty;
        }
        public IActionResult Index()
        {
            return View();
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
