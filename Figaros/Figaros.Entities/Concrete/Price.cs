using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Price : EntityBase, IEntity
    {
        public string Service { get; set; }
        public string Description { get; set; }
        public double ServicePrice { get; set; }

        public IList<Booking> Bookings { get; set; }
    }
}
