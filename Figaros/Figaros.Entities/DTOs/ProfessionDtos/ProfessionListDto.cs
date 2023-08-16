using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.ProfessionDtos
{
    public class ProfessionListDto : DtoGetBase
    {
        public IList<Profession> Professions { get; set; }
    }
}
