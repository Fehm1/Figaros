using Figaros.Entities.DTOs.ServiceDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IServiceService
    {
        Task<IDataResult<ServiceDto>> Get(int ServiceId);
        Task<IDataResult<ServiceListDto>> GetAll();
        Task<IDataResult<ServiceListDto>> GetAllByNonDeleted();
        Task<IDataResult<ServiceListDto>> GetAllByDeleted();
        Task<IDataResult<ServiceDto>> Add(ServicePostDto ServicePostDto);
        Task<IDataResult<ServiceDto>> Update(ServiceUpdateDto ServiceUpdateDto);
        Task<IDataResult<ServiceDto>> Restore(int ServiceId);
        Task<IDataResult<ServiceDto>> Delete(int ServiceId);
        Task<IDataResult<ServiceDto>> HardDelete(int ServiceId);
    }
}
