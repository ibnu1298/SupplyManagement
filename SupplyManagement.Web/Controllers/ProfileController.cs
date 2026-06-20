using Microsoft.AspNetCore.Mvc;
using SupplyManagement.Web.Filters;

namespace SupplyManagement.Web.Controllers
{
    [JwtRequired]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
