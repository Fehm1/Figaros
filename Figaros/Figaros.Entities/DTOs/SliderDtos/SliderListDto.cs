using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.SliderDtos
{
    public class SliderListDto : DtoGetBase
    {
        public IList<Slider> Sliders { get; set; }
    }
}
