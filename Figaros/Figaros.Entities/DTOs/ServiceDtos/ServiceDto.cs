using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.ServiceDtos
{
    public class ServiceDto : DtoGetBase
    {
        public Service Service { get; set; }
    }
}
