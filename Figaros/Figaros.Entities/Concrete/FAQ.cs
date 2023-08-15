using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class FAQ : EntityBase, IEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
