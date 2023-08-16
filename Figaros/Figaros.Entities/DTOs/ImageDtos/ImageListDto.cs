using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.ImageDtos
{
    public class ImageListDto : DtoGetBase
    {
        public IList<Image> Images { get; set; }
    }
}
