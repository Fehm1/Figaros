using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Slider : EntityBase, IEntity
    {
        public string ImageString { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RedirectUrl { get; set; }
    }
}
