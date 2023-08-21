using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.DTOs.ImageDtos;
using Figaros.Entities.DTOs.SettingDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Extentions;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace Figaros.Services.Concrete
{
    public class SettingManager : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SettingManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IDataResult<SettingDto>> Get(int settingId)
        {
            var setting = await _unitOfWork.Settings.GetAsync(x => x.Id == settingId);

            if (setting != null)
            {
                return new DataResult<SettingDto>(ResultStatus.Success, new SettingDto
                {
                    Setting = setting,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<SettingDto>(ResultStatus.Error, "Şəkil tapılmadı!", new SettingDto
            {
                Setting = null,
                ResultStatus = ResultStatus.Error,
                Message = "Şəkil tapılmadı!"
            });
        }

        public async Task<IDataResult<SettingUpdateDto>> GetUpdateDto(int settingId)
        {
            var result = await _unitOfWork.Settings.AnyAsync(c => c.Id == settingId);
            if (result)
            {
                var setting = await _unitOfWork.Settings.GetAsync(c => c.Id == settingId);
                SettingUpdateDto settingUpdateDto = _mapper.Map<SettingUpdateDto>(setting);
                return new DataResult<SettingUpdateDto>(ResultStatus.Success, settingUpdateDto);
            }
            else
            {
                return new DataResult<SettingUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<SettingDto>> Update(SettingUpdateDto settingUpdateDto)
        {
            var setting = await _unitOfWork.Settings.GetAsync(x => x.Id == settingUpdateDto.Id);

            if (setting != null)
            {
                if (settingUpdateDto.HeaderLogoFile != null)
                {
                    if (!settingUpdateDto.HeaderLogoFile.IsImageContent())
                    {
                        return new DataResult<SettingDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new SettingDto
                        {
                            Setting = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!settingUpdateDto.HeaderLogoFile.IsValidImageLength())
                    {
                        return new DataResult<SettingDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new SettingDto
                        {
                            Setting = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = settingUpdateDto.HeaderLogoFile.SaveImage(_env.WebRootPath, "uploads/Settings");
                    setting.HeaderLogo.DeleteImage(_env.WebRootPath, "uploads/Settings");

                    setting.HeaderLogo = newImage;
                }

                if (settingUpdateDto.FooterLogoFile != null)
                {
                    if (!settingUpdateDto.FooterLogoFile.IsImageContent())
                    {
                        return new DataResult<SettingDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new SettingDto
                        {
                            Setting = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!settingUpdateDto.FooterLogoFile.IsValidImageLength())
                    {
                        return new DataResult<SettingDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new SettingDto
                        {
                            Setting = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = settingUpdateDto.FooterLogoFile.SaveImage(_env.WebRootPath, "uploads/Settings");
                    setting.FooterLogo.DeleteImage(_env.WebRootPath, "uploads/Settings");

                    setting.FooterLogo = newImage;
                }

                setting.IsActive = settingUpdateDto.IsActive;
                setting.ModifiedDate = DateTime.Now;

                var updatedSetting = await _unitOfWork.Settings.UpdateAsync(setting);
                await _unitOfWork.SaveAsync();

                return new DataResult<SettingDto>(ResultStatus.Success, "Şəkil uğurla yeniləndi!", new SettingDto
                {
                    Setting = updatedSetting,
                    ResultStatus = ResultStatus.Error,
                    Message = "Şəkil uğurla yeniləndi!"
                });
            }

            return new DataResult<SettingDto>(ResultStatus.Error, "Şəkil tapılmadı!", new SettingDto
            {
                Setting = null,
                ResultStatus = ResultStatus.Error,
                Message = "Şəkil tapılmadı!"
            });
        }
    }
}
