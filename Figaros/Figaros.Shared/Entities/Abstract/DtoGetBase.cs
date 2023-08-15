using Figaros.Shared.Utilities.Results.ComplexTypes;

namespace Figaros.Entities.Abstract
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }
    }
}
