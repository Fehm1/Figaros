using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.PriceDtos
{
    public class PriceDto : DtoGetBase
    {
        public Price Price { get; set; }
    }
}
