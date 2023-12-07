using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Booking : EntityBase, IEntity
    {
        public int EmployeeId { get; set; }
        public int PriceId { get; set; }
        public int TimeId { get; set; }

        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool IsCompleted { get; set; } = false;

        public string BookingTime { get; set; }

        public Employee Employee { get; set; }
        public Price Price { get; set; }
        public Time Time { get; set; }
    }
}
