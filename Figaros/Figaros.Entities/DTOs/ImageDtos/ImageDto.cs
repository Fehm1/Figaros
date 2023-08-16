using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.ImageDtos
{
    public class ImageDto : DtoGetBase
    {
        public Image Image { get; set; }
    }
}
