using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.TimeDtos
{
    public class TimeDto : DtoGetBase
    {
        public Time Time { get; set; }
    }
}
