using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Entities.DTOs.TimeDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface ITimeService
    {
        Task<IDataResult<TimeDto>> Get(int TimeId);
        Task<IDataResult<TimeUpdateDto>> GetUpdateDto(int TimeId);
        Task<IDataResult<TimeListDto>> GetAll();
        Task<IDataResult<TimeListDto>> GetAllByNonDeleted();
        Task<IDataResult<TimeListDto>> GetAllByDeleted();
        Task<IDataResult<TimeDto>> Add(TimePostDto TimePostDto);
        Task<IDataResult<TimeDto>> Update(TimeUpdateDto TimeUpdateDto);
        Task<IDataResult<TimeDto>> Active(int TimeId);
        Task<IDataResult<TimeDto>> Deactive(int TimeId);
    }
}
