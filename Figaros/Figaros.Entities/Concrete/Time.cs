using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Time : EntityBase, IEntity
    {
        public string Hour { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
