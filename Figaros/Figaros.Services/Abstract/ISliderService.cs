using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Entities.DTOs.SliderDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface ISliderService
    {
        Task<IDataResult<SliderDto>> Get(int SliderId);
        Task<IDataResult<SliderUpdateDto>> GetUpdateDto(int SliderId);
        Task<IDataResult<SliderListDto>> GetAll();
        Task<IDataResult<SliderListDto>> GetAllByNonDeleted();
        Task<IDataResult<SliderListDto>> GetAllByDeleted();
        Task<IDataResult<SliderDto>> Add(SliderPostDto SliderPostDto);
        Task<IDataResult<SliderDto>> Update(SliderUpdateDto SliderUpdateDto);
        Task<IDataResult<SliderDto>> Restore(int SliderId);
        Task<IDataResult<SliderDto>> Delete(int SliderId);
        Task<IDataResult<SliderDto>> HardDelete(int SliderId);
    }
}
