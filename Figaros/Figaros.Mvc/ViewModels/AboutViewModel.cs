using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.BookingDtos;

namespace Figaros.Mvc.ViewModels
{
    public class AboutViewModel
    {
        public About About { get; set; }
        public IList<Employee> Employees { get; set; }
        public BookingPostDto Booking { get; set; }
    }
}
