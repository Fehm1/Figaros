using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.OurSalonDtos
{
    public class OurSalonListDto : DtoGetBase
    {
        public IList<OurSalon> OurSalons { get; set; }
    }
}
