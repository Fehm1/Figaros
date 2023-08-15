using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class About : EntityBase, IEntity
    {
        public string ImageString { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
