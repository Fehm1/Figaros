using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.PriceDtos
{
    public class PriceListDto : DtoGetBase
    {
        public IList<Price> Prices { get; set; }
    }
}
