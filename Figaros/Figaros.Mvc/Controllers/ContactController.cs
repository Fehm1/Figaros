using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
