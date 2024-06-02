using Figaros.Data.Concrete.EntityFramework.Contexts;
using Figaros.Services.Services.Abstract;
using Figaros.Shared.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Figaros.Services.Services.Concrete
{
    public class ManageLayoutService : IManageLayoutService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ManageLayoutService(AppDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }
        public async Task<AppUser> GetAdmin()
        {
            AppUser admin = null;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                admin = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
                return admin;
            }
            return null;
        }

    }
}
