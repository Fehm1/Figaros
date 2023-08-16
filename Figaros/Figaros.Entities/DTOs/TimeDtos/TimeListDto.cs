using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.TimeDtos
{
    public class TimeListDto : DtoGetBase
    {
        public IList<Time> Times { get; set; }
    }
}
