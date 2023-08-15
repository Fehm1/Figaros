using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Booking : EntityBase, IEntity
    {
        public int EmployeeId { get; set; }
        public int ServiceId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Message { get; set; }

        public Employee Employee { get; set; }
        public Service Service { get; set; }
    }
}
