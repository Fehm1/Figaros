using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.SponsorDtos
{
    public class SponsorListDto : DtoGetBase
    {
        public IList<Sponsor> Sponsors { get; set; }
    }
}
