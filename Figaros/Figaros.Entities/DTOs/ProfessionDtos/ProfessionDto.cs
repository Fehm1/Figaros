using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.ProfessionDtos
{
    public class ProfessionDto : DtoGetBase
    {
        public Profession Profession { get; set; }
    }
}
