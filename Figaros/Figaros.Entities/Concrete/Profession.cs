using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Profession : EntityBase, IEntity
    {
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
