using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.BookingDtos;

namespace Figaros.Mvc.ViewModels
{
    public class ProductViewModel
    {
        public IList<Product> Products { get; set; }
        public BookingPostDto Booking { get; set; }
    }
}
