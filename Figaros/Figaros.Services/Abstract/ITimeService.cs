using Figaros.Entities.DTOs.TimeDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface ITimeService
    {
        Task<IDataResult<TimeDto>> Get(int TimeId);
        Task<IDataResult<TimeListDto>> GetAll();
        Task<IDataResult<TimeListDto>> GetAllByNonDeleted();
        Task<IDataResult<TimeListDto>> GetAllByDeleted();
        Task<IDataResult<TimeDto>> Add(TimePostDto TimePostDto);
        Task<IDataResult<TimeDto>> Update(TimeUpdateDto TimeUpdateDto);
        Task<IDataResult<TimeDto>> Restore(int TimeId);
        Task<IDataResult<TimeDto>> Delete(int TimeId);
        Task<IDataResult<TimeDto>> HardDelete(int TimeId);
    }
}
