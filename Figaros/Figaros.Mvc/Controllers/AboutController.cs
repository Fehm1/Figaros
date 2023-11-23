using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
