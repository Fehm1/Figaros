using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.BookingDtos
{
    public class BookingListDto : DtoGetBase
    {
        public IList<Booking> Bookings { get; set; }
    }
}
