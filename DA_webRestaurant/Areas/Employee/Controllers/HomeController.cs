using Microsoft.AspNetCore.Mvc;

namespace DA_webRestaurant.Areas.Employee.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index1()
        {
            return View();
        }
    }
}
