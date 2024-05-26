using Figaros.Data.Concrete.EntityFramework.Contexts;
using Figaros.Entities.DTOs.UserDtos;
using Figaros.Shared.Entities.Concrete;
using Figaros.Shared.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;

namespace EraLogisticMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public UserController(AppDbContext context, RoleManager<IdentityRole> roleManager, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _env = env;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.Include(x => x.Role).ToListAsync();

            ViewBag.Roles = await _context.Roles.ToListAsync();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Roles = await _context.Roles.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserPostDto userPostDto)
        {
            ViewBag.Roles = await _context.Roles.ToListAsync();

            List<IdentityRole> roles = await _context.Roles.ToListAsync();

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    RoleId = userPostDto.RoleId,
                    Name = userPostDto.Name,
                    Surname = userPostDto.Surname,
                    UserName = userPostDto.UserName,
                    Email = userPostDto.Email,
                    PhoneNumber = userPostDto.Phone,
                    IsActive = true,
                    IsDeleted = false
                };

                if (roles.FirstOrDefault(x => x.Id == userPostDto.RoleId).Name == "Super Admin")
                {
                    user.IsSuperAdmin = true;
                }
                else if (roles.FirstOrDefault(x => x.Id == userPostDto.RoleId).Name == "Admin")
                {
                    user.IsAdmin = true;
                }


                if (userPostDto.ImageFile != null)
                {
                    if (!userPostDto.ImageFile.IsImageContent())
                    {
                        return View(userPostDto);
                    }

                    if (!userPostDto.ImageFile.IsValidImageLength())
                    {
                        return View(userPostDto);
                    }

                    string newImage = userPostDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Users");
                    user.Image = newImage;
                }
                else
                {
                    return View(userPostDto);
                }

                var result = await _userManager.CreateAsync(user, userPostDto.Password);

                var role = roles.FirstOrDefault(x => x.Id == userPostDto.RoleId).Name;

                await _userManager.AddToRoleAsync(user, role);

                return RedirectToAction("index");
            }

            return View(userPostDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            ViewBag.Roles = await _context.Roles.ToListAsync();

            AppUser user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);

            UserUpdateDto userUpdateDto = new UserUpdateDto
            {
                Id = id,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                RoleId = user.RoleId,
                IsActive = user.IsActive,
                ImageString = user.Image,
                IsAdmin = user.IsAdmin,
                IsSuperAdmin = user.IsSuperAdmin
            };

            return View(userUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            ViewBag.Roles = await _context.Roles.ToListAsync();

            List<IdentityRole> roles = await _context.Roles.ToListAsync();

            if (ModelState.IsValid)
            {
                AppUser user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == userUpdateDto.Id);

                if (user != null)
                {
                    user.UserName = userUpdateDto.UserName;
                    user.Name = userUpdateDto.Name;
                    user.Surname = userUpdateDto.Surname;
                    user.PhoneNumber = userUpdateDto.Phone;
                    user.Email = userUpdateDto.Email;
                    user.IsActive = userUpdateDto.IsActive;
                }

                var oldRole = roles.FirstOrDefault(x => x.Id == user.RoleId);
                var newRole = roles.FirstOrDefault(x => x.Id == userUpdateDto.RoleId);

                if (newRole.Name == "Super Admin")
                {
                    user.IsSuperAdmin = true;
                    user.IsAdmin = false;
                }
                else if (newRole.Name == "Admin")
                {
                    user.IsAdmin = true;
                    user.IsSuperAdmin = false;
                }
                else
                {
                    user.IsAdmin = false;
                    user.IsSuperAdmin = false;
                }

                await _userManager.RemoveFromRoleAsync(user, oldRole.Name);
                await _userManager.AddToRoleAsync(user, newRole.Name);

                user.RoleId = newRole.Id;

                if (userUpdateDto.ImageFile != null)
                {
                    if (!userUpdateDto.ImageFile.IsImageContent())
                    {
                        return View(userUpdateDto);
                    }

                    if (!userUpdateDto.ImageFile.IsValidImageLength())
                    {
                        return View(userUpdateDto);
                    }

                    string newImage = userUpdateDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Users");
                    user.Image.DeleteImage(_env.WebRootPath, "uploads/Users");
                    user.Image = newImage;
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            ChangePasswordDto passwordDto = new ChangePasswordDto
            {
                Id = id
            };

            return View(passwordDto);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto passwordDto)
        {
            if (passwordDto == null)
            {
                return View(passwordDto);
            }

            AppUser user = await _context.Users.FirstOrDefaultAsync(x => x.Id == passwordDto.Id);

            if (ModelState.IsValid)
            {
                if (user.PasswordHash == HashPassword(passwordDto.OldPassword))
                {
                    return View(passwordDto);
                }
                else
                {
                    user.PasswordHash = HashPassword(passwordDto.OldPassword);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

        public string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);

            var hashedPassword = hash.ComputeHash(passwordBytes);

            return Convert.ToHexString(hashedPassword);
        }
    }
}
