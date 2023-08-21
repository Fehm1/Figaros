using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
