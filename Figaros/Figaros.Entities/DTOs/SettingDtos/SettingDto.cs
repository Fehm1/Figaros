using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.SettingDtos
{
    public class SettingDto : DtoGetBase
    {
        public Setting Setting { get; set; }
    }
}
