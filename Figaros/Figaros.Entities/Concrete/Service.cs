using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Service : EntityBase, IEntity
    {
        public string ImageString { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Booking> Bookings { get; set; }
        //homepagede de istifade edersen
    }
}
