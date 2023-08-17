using Figaros.Entities.DTOs.SettingDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface ISettingService
    {
        Task<IDataResult<SettingDto>> Get(int settingId);
        Task<IDataResult<SettingUpdateDto>> GetUpdateDto(int settingId);
        Task<IDataResult<SettingDto>> Update(SettingUpdateDto settingUpdateDto);
    }
}
