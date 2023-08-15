using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Employee : EntityBase, IEntity
    {
        public int ProfessionId { get; set; }
        public string ImageString { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string InstagramUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string TiktokUrl { get; set; }
        public string WhatsAppUrl { get; set; }

        public Profession Profession { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
