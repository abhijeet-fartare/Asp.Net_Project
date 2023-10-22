using Microsoft.AspNetCore.Mvc;

namespace SmartContactsManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Route("admin/home/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
