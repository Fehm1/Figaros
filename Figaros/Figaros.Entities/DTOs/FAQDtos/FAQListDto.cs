using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.FAQDtos
{
    public class FAQListDto : DtoGetBase
    {
        public IList<FAQ> FAQs { get; set; }
    }
}
