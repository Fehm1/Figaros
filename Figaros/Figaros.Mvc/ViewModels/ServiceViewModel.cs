using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.BookingDtos;

namespace Figaros.Mvc.ViewModels
{
    public class ServiceViewModel
    {
        public IList<Service> Services { get; set; }
        public IList<Price> Prices { get; set; }
        public BookingPostDto Booking { get; set; }
    }
}
