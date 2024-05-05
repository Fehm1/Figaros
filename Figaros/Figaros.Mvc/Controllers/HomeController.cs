using Figaros.Data.Concrete.EntityFramework.Contexts;
using Figaros.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Figaros.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Sliders = await _context.Sliders.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync(),
                About = await _context.About.FirstOrDefaultAsync(),
                Services = await _context.Services.Where(x => x.IsDeleted == false && x.IsActive == true && x.IsPoster == true).ToListAsync(),
                Employees = await _context.Employees.Include(x => x.Profession).Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync(),
                Images = await _context.Images.ToListAsync(),
                Prices = await _context.Prices.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync(),
                Settings = await _context.Settings.FirstOrDefaultAsync(x => x.Id == 2)
            };

            ViewBag.Times = await _context.Times.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
            ViewBag.Employees = await _context.Employees.Include(x => x.Profession).Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
            ViewBag.Services = await _context.Prices.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
            ViewBag.Settings = await _context.Settings.FirstOrDefaultAsync(x => x.Id == 2);

            return View(homeViewModel);
        }
    }
}