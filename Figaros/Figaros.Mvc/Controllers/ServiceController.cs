using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
