using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.FAQDtos
{
    public class FAQDto : DtoGetBase
    {
        public FAQ FAQ { get; set; }
    }
}
