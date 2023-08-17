using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Service : EntityBase, IEntity
    {
        public string ImageString { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPoster { get; set; }

        //homepagede de istifade edersen
    }
}
