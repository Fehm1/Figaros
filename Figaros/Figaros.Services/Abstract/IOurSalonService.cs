using Figaros.Entities.DTOs.OurSalonDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IOurSalonService
    {
        Task<IDataResult<OurSalonDto>> Get(int OurSalonId);
        Task<IDataResult<OurSalonListDto>> GetAll();
        Task<IDataResult<OurSalonListDto>> GetAllByNonDeleted();
        Task<IDataResult<OurSalonListDto>> GetAllByDeleted();
        Task<IDataResult<OurSalonDto>> Update(OurSalonUpdateDto OurSalonUpdateDto);
        Task<IDataResult<OurSalonDto>> Restore(int OurSalonId);
        Task<IDataResult<OurSalonDto>> Delete(int OurSalonId);
        Task<IDataResult<OurSalonDto>> HardDelete(int OurSalonId);
    }
}
