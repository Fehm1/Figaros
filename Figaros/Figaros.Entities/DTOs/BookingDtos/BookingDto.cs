using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.BookingDtos
{
    public class BookingDto : DtoGetBase
    {
        public Booking Booking { get; set; }
    }
}
