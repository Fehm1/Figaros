using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Sponsor : EntityBase, IEntity
    {
        public string CompanyImageString { get; set; }
    }
}
