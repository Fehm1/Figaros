using Microsoft.AspNetCore.Identity;

namespace Figaros.Shared.Entities.Concrete
{
    public class AppUser : IdentityUser
    {
        public string RoleId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public bool IsAdmin { get; set; } = false;
        public bool IsSuperAdmin { get; set; } = false;
    }
}
