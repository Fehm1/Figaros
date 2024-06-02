using Figaros.Entities.Abstract;
using Figaros.Shared.Entities.Concrete;

namespace Figaros.Entities.DTOs.UserDtos
{
    public class UserDto : DtoGetBase
    {
        public AppUser User { get; set; }
    }
}
