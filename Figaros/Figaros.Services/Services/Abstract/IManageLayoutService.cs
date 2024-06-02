using Figaros.Shared.Entities.Concrete;

namespace Figaros.Services.Services.Abstract
{
    public interface IManageLayoutService
    {
        public Task<AppUser> GetAdmin();
    }
}
