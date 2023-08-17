using Figaros.Entities.DTOs.OurSalonDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IOurSalonService
    {
        Task<IDataResult<OurSalonDto>> Get(int OurSalonId);
        Task<IDataResult<OurSalonUpdateDto>> GetUpdateDto(int OurSalonId);
        Task<IDataResult<OurSalonListDto>> GetAll();
        Task<IDataResult<OurSalonListDto>> GetAllByNonDeleted();
        Task<IDataResult<OurSalonListDto>> GetAllByDeleted();
        Task<IDataResult<OurSalonDto>> Update(OurSalonUpdateDto OurSalonUpdateDto);
    }
}
