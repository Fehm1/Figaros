using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.ProductDtos
{
    public class ProductListDto : DtoGetBase
    {
        public IList<Product> Products { get; set; }
    }
}
