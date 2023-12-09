using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.DTOs.AboutDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Extentions;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace Figaros.Services.Concrete
{
    public class AboutManager : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AboutManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IDataResult<AboutDto>> Get(int AboutId)
        {
            var About = await _unitOfWork.About.GetAsync(x => x.Id == AboutId);

            if (About != null)
            {
                return new DataResult<AboutDto>(ResultStatus.Success, new AboutDto
                {
                    About = About,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AboutDto>(ResultStatus.Error, "Məlumat tapılmadı!", new AboutDto
            {
                About = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məlumatlar tapılmadı!"
            });
        }

        public async Task<IDataResult<AboutUpdateDto>> GetUpdateDto(int AboutId)
        {
            var result = await _unitOfWork.About.AnyAsync(c => c.Id == AboutId);
            if (result)
            {
                var About = await _unitOfWork.About.GetAsync(c => c.Id == AboutId);
                var AboutUpdateDto = _mapper.Map<AboutUpdateDto>(About);
                return new DataResult<AboutUpdateDto>(ResultStatus.Success, AboutUpdateDto);
            }
            else
            {
                return new DataResult<AboutUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<AboutDto>> Update(AboutUpdateDto aboutUpdateDto)
        {
            var about = await _unitOfWork.About.GetAsync(x => x.Id == aboutUpdateDto.Id);

            if (about != null)
            {
                if (aboutUpdateDto.BigImageFile != null)
                {
                    if (!aboutUpdateDto.BigImageFile.IsImageContent())
                    {
                        return new DataResult<AboutDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new AboutDto
                        {
                            About = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!aboutUpdateDto.BigImageFile.IsValidImageLength())
                    {
                        return new DataResult<AboutDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new AboutDto
                        {
                            About = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = aboutUpdateDto.BigImageFile.SaveImage(_env.WebRootPath, "uploads/About");
                    about.BigImageString.DeleteImage(_env.WebRootPath, "uploads/About");

                    about.BigImageString = newImage;
                }

                if (aboutUpdateDto.SmallImageFile != null)
                {
                    if (!aboutUpdateDto.SmallImageFile.IsImageContent())
                    {
                        return new DataResult<AboutDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new AboutDto
                        {
                            About = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!aboutUpdateDto.SmallImageFile.IsValidImageLength())
                    {
                        return new DataResult<AboutDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new AboutDto
                        {
                            About = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = aboutUpdateDto.SmallImageFile.SaveImage(_env.WebRootPath, "uploads/About");
                    about.SmallImageString.DeleteImage(_env.WebRootPath, "uploads/About");

                    about.SmallImageString = newImage;
                }

                about.Title = aboutUpdateDto.Title;
                about.Description = aboutUpdateDto.Description;
                about.IsActive = aboutUpdateDto.IsActive;
                about.ModifiedDate = DateTime.Now;

                var updatedAbout = await _unitOfWork.About.UpdateAsync(about);
                await _unitOfWork.SaveAsync();

                return new DataResult<AboutDto>(ResultStatus.Success, "Məlumatlar uğurla yeniləndi!", new AboutDto
                {
                    About = updatedAbout,
                    ResultStatus = ResultStatus.Error,
                    Message = "Məlumatlar uğurla yeniləndi!"
                });
            }

            return new DataResult<AboutDto>(ResultStatus.Error, "Məlumatlar tapılmadı!", new AboutDto
            {
                About = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məlumatlar tapılmadı!"
            });
        }
    }
}
