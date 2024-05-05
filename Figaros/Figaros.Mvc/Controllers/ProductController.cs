using Figaros.Data.Concrete.EntityFramework.Contexts;
using Figaros.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Figaros.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                Products = await _context.Products.Where(x => !x.IsDeleted && x.IsActive).ToListAsync()
            };

            ViewBag.Times = await _context.Times.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
            ViewBag.Employees = await _context.Employees.Include(x => x.Profession).Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
            ViewBag.Services = await _context.Prices.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
            ViewBag.Settings = await _context.Settings.FirstOrDefaultAsync(x => x.Id == 2);

            return View(productViewModel);
        }
    }
}
