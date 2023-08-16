using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.ServiceDtos
{
    public class ServiceListDto : DtoGetBase
    {
        public IList<Service> Services { get; set; }
    }
}
