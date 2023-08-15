using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Image : EntityBase, IEntity
    {
        public string ImageString { get; set; }
    }
}
