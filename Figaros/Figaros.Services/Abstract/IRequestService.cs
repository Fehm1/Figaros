using Figaros.Entities.DTOs.RequestDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IRequestService
    {
        Task<IDataResult<RequestDto>> Get(int RequestId);
        Task<IDataResult<RequestListDto>> GetAll();
        Task<IDataResult<RequestListDto>> GetAllByNonDeleted();
        Task<IDataResult<RequestListDto>> GetAllByDeleted();
        Task<IDataResult<RequestDto>> Add(RequestPostDto RequestPostDto);
        Task<IDataResult<RequestDto>> Restore(int RequestId);
        Task<IDataResult<RequestDto>> Readed(int RequestId);
        Task<IDataResult<RequestDto>> Delete(int RequestId);
        Task<IDataResult<RequestDto>> HardDelete(int RequestId);
    }
}
