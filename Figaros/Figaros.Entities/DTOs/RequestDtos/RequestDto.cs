using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.RequestDtos
{
    public class RequestDto : DtoGetBase
    {
        public Request Request { get; set; }
    }
}
