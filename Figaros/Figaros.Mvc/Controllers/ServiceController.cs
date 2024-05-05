using Figaros.Data.Concrete.EntityFramework.Contexts;
using Figaros.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Figaros.Mvc.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                Services =  await _context.Services.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync(),
                Prices = await _context.Prices.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync()
            };

            ViewBag.Times = await _context.Times.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
            ViewBag.Employees = await _context.Employees.Include(x => x.Profession).Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
            ViewBag.Services = await _context.Prices.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
            ViewBag.Settings = await _context.Settings.FirstOrDefaultAsync(x => x.Id == 2);

            return View(serviceViewModel);
        }
    }
}
