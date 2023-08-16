using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.SponsorDtos
{
    public class SponsorDto : DtoGetBase
    {
        public Sponsor Sponsor { get; set; }
    }
}
