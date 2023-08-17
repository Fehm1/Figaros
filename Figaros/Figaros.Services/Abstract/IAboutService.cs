using Figaros.Entities.DTOs.AboutDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IAboutService
    {
        Task<IDataResult<AboutDto>> Get(int AboutId);
        Task<IDataResult<AboutUpdateDto>> GetUpdateDto(int AboutId);
        Task<IDataResult<AboutDto>> Update(AboutUpdateDto aboutUpdateDto);
    }
}
