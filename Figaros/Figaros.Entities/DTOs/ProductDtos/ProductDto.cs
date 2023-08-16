using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.ProductDtos
{
    public class ProductDto : DtoGetBase
    {
        public Product Product { get; set; }
    }
}
