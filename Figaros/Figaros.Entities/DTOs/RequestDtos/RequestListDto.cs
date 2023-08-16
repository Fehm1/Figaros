using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.RequestDtos
{
    public class RequestListDto : DtoGetBase
    {
        public IList<Request> Requests { get; set; }
    }
}
