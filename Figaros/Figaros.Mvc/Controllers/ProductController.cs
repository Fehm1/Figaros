using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
