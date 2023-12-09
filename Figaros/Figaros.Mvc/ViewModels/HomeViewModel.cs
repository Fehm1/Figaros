using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.BookingDtos;

namespace Figaros.Mvc.ViewModels
{
    public class HomeViewModel
    {
        public IList<Slider> Sliders { get; set; }
        public About About { get; set; }
        public IList<Service> Services { get; set; }
        public IList<Price> Prices { get; set; }
        public IList<Employee> Employees { get; set; }
        public Setting Settings { get; set; }
        public BookingPostDto Booking { get; set; }
    }
}
