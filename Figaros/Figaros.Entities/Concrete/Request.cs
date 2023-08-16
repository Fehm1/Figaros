using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Request : EntityBase, IEntity
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CV { get; set; }
        public string IntagramURl { get; set; }
        public string TiktokUrl { get; set; }
        public string Message { get; set; }
    }
}
