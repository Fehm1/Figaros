using Figaros.Data.Concrete.EntityFramework.Contexts;
using Figaros.Entities.DTOs.RoleDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EraLogisticMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class RoleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();

            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RolePostDto rolePostDto)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole(rolePostDto.Name);

                var result = await _roleManager.CreateAsync(role);

                return RedirectToAction("index");
            }

            return View(rolePostDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var result = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                RoleUpdateDto role = new RoleUpdateDto
                {
                    Id = id,
                    Name = result.Name
                };

                return View(role);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RoleUpdateDto roleUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.Roles.FirstOrDefaultAsync(x => x.Id == roleUpdateDto.Id);
                if (result != null)
                {
                    result.Name = roleUpdateDto.Name;
                    result.NormalizedName = roleUpdateDto.Name.ToUpper();

                    await _context.SaveChangesAsync();

                    return RedirectToAction("index");
                }
            }

            return View(roleUpdateDto);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var result = _context.Roles.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                await Task.Run(() => { _context.Roles.Remove(result); });
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
