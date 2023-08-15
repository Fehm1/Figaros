using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T> : IResult
    {
        public T Data { get; }
    }
}
