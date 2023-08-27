using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.SliderDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Extentions;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace Figaros.Services.Concrete
{
    public class SliderManager : ISliderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SliderManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IDataResult<SliderDto>> Add(SliderPostDto SliderPostDto)
        {
            if (SliderPostDto != null)
            {
                var slider = _mapper.Map<Slider>(SliderPostDto);

                if (SliderPostDto.ImageFile != null)
                {
                    if (!SliderPostDto.ImageFile.IsImageContent())
                    {
                        return new DataResult<SliderDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new SliderDto
                        {
                            Slider = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!SliderPostDto.ImageFile.IsValidImageLength())
                    {
                        return new DataResult<SliderDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new SliderDto
                        {
                            Slider = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = SliderPostDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Sliders");
                    slider.ImageString = newImage;
                }
                else
                {
                    return new DataResult<SliderDto>(ResultStatus.Error, "Şəkil daxil edin!", new SliderDto
                    {
                        Slider = null,
                        ResultStatus = ResultStatus.Error,
                        Message = "Şəkil daxil edin!"
                    });
                }


                var addedSlider = await _unitOfWork.Sliders.AddAsync(slider);
                await _unitOfWork.SaveAsync();
                SliderDto sliderDto = _mapper.Map<SliderDto>(slider);

                return new DataResult<SliderDto>(ResultStatus.Success, "Slayder uğurla əlavə olundu!", new SliderDto
                {
                    Slider = addedSlider,
                    ResultStatus = ResultStatus.Success,
                    Message = "Slayder uğurla əlavə olundu!"
                });
            }

            return new DataResult<SliderDto>(ResultStatus.Error, "Slayder əlavə olunmadı!", new SliderDto
            {
                Slider = null,
                ResultStatus = ResultStatus.Error,
                Message = "Slayder əlavə olunmadı!"
            });
        }

        public async Task<IDataResult<SliderDto>> Delete(int SliderId)
        {
            var slider = await _unitOfWork.Sliders.GetAsync(x => x.Id == SliderId);

            if (slider != null)
            {
                slider.IsDeleted = true;
                var deletedSlider = await _unitOfWork.Sliders.UpdateAsync(slider);
                await _unitOfWork.SaveAsync();

                return new DataResult<SliderDto>(ResultStatus.Success, "Slayder uğurla silindi!", new SliderDto
                {
                    Slider = deletedSlider,
                    ResultStatus = ResultStatus.Success,
                    Message = "Slayder uğurla silindi!"
                });
            }

            return new DataResult<SliderDto>(ResultStatus.Error, "Slayder tapılmadı!", new SliderDto
            {
                Slider = null,
                ResultStatus = ResultStatus.Error,
                Message = "Slayder tapılmadı!"
            });
        }

        public async Task<IDataResult<SliderDto>> Get(int SliderId)
        {
            var slider = await _unitOfWork.Sliders.GetAsync(x => x.Id == SliderId);

            if (slider != null)
            {
                return new DataResult<SliderDto>(ResultStatus.Success, new SliderDto
                {
                    Slider = slider,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<SliderDto>(ResultStatus.Error, "Slayder tapılmadı!", new SliderDto
            {
                Slider = null,
                ResultStatus = ResultStatus.Error,
                Message = "Slayder tapılmadı!"
            });
        }

        public async Task<IDataResult<SliderListDto>> GetAll()
        {
            var sliders = await _unitOfWork.Sliders.GetAllAsync();

            if (sliders.Count >= 0)
            {
                return new DataResult<SliderListDto>(ResultStatus.Success, new SliderListDto
                {
                    Sliders = sliders,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<SliderListDto>(ResultStatus.Error, "Slayderlar tapılmadı!", new SliderListDto
            {
                Sliders = null,
                ResultStatus = ResultStatus.Error,
                Message = "Slayderlar tapılmadı!"
            });
        }

        public async Task<IDataResult<SliderListDto>> GetAllByDeleted()
        {
            var sliders = await _unitOfWork.Sliders.GetAllAsync(x => x.IsDeleted);

            if (sliders.Count >= 0)
            {
                return new DataResult<SliderListDto>(ResultStatus.Success, new SliderListDto
                {
                    Sliders = sliders,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<SliderListDto>(ResultStatus.Error, "Slayderlar tapılmadı!", new SliderListDto
            {
                Sliders = null,
                ResultStatus = ResultStatus.Error,
                Message = "Slayderlar tapılmadı!"
            });
        }

        public async Task<IDataResult<SliderListDto>> GetAllByNonDeleted()
        {
            var sliders = await _unitOfWork.Sliders.GetAllAsync(x => !x.IsDeleted);

            if (sliders.Count >= 0)
            {
                return new DataResult<SliderListDto>(ResultStatus.Success, new SliderListDto
                {
                    Sliders = sliders,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<SliderListDto>(ResultStatus.Error, "Slayderlar tapılmadı!", new SliderListDto
            {
                Sliders = null,
                ResultStatus = ResultStatus.Error,
                Message = "Slayderlar tapılmadı!"
            });
        }

        public async Task<IDataResult<SliderUpdateDto>> GetUpdateDto(int SliderId)
        {
            var result = await _unitOfWork.Sliders.AnyAsync(c => c.Id == SliderId);
            if (result)
            {
                var slider = await _unitOfWork.Sliders.GetAsync(c => c.Id == SliderId);
                SliderUpdateDto sliderUpdateDto = _mapper.Map<SliderUpdateDto>(slider);
                return new DataResult<SliderUpdateDto>(ResultStatus.Success, sliderUpdateDto);
            }
            else
            {
                return new DataResult<SliderUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<SliderDto>> HardDelete(int SliderId)
        {
            var slider = await _unitOfWork.Sliders.GetAsync(x => x.Id == SliderId);

            if (slider != null)
            { 
                var deletedSlider = await _unitOfWork.Sliders.DeleteAsync(slider);
                slider.ImageString.DeleteImage(_env.WebRootPath, "uploads/Sliders");
                await _unitOfWork.SaveAsync();

                return new DataResult<SliderDto>(ResultStatus.Success, "Slayder uğurla silindi!", new SliderDto
                {
                    Slider = deletedSlider,
                    ResultStatus = ResultStatus.Success,
                    Message = "Slayder uğurla silindi!"
                });
            }

            return new DataResult<SliderDto>(ResultStatus.Error, "Slayder tapılmadı!", new SliderDto
            {
                Slider = null,
                ResultStatus = ResultStatus.Error,
                Message = "Slayder tapılmadı!"
            });
        }

        public async Task<IDataResult<SliderDto>> Restore(int SliderId)
        {
            var slider = await _unitOfWork.Sliders.GetAsync(x => x.Id == SliderId);

            if (slider != null)
            {
                slider.IsDeleted = false;
                var deletedSlider = await _unitOfWork.Sliders.UpdateAsync(slider);
                await _unitOfWork.SaveAsync();

                return new DataResult<SliderDto>(ResultStatus.Success, "Slayder uğurla geri qaytarıldı!", new SliderDto
                {
                    Slider = deletedSlider,
                    ResultStatus = ResultStatus.Success,
                    Message = "Slayder uğurla geri qaytarıldı!"
                });
            }

            return new DataResult<SliderDto>(ResultStatus.Error, "Slayder tapılmadı!", new SliderDto
            {
                Slider = null,
                ResultStatus = ResultStatus.Error,
                Message = "Slayder tapılmadı!"
            });
        }

        public async Task<IDataResult<SliderDto>> Update(SliderUpdateDto SliderUpdateDto)
        {
            var slider = await _unitOfWork.Sliders.GetAsync(x => x.Id == SliderUpdateDto.Id);

            if (slider != null)
            {
                if (SliderUpdateDto.ImageFile != null)
                {
                    if (!SliderUpdateDto.ImageFile.IsImageContent())
                    {
                        return new DataResult<SliderDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new SliderDto
                        {
                            Slider = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!SliderUpdateDto.ImageFile.IsValidImageLength())
                    {
                        return new DataResult<SliderDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new SliderDto
                        {
                            Slider = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = SliderUpdateDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Sliders");
                    slider.ImageString.DeleteImage(_env.WebRootPath, "uploads/Sliders");

                    slider.ImageString = newImage;
                }

                slider.Title = SliderUpdateDto.Title;
                slider.Description = SliderUpdateDto.Description;
                slider.RedirectUrl = SliderUpdateDto.RedirectUrl;
                slider.IsActive = SliderUpdateDto.IsActive;
                slider.ModifiedDate = DateTime.Now;

                var updatedSlider = await _unitOfWork.Sliders.UpdateAsync(slider);
                await _unitOfWork.SaveAsync();

                return new DataResult<SliderDto>(ResultStatus.Success, "Slayder uğurla yeniləndi!", new SliderDto
                {
                    Slider = updatedSlider,
                    ResultStatus = ResultStatus.Error,
                    Message = "Slayder uğurla yeniləndi!"
                });
            }

            return new DataResult<SliderDto>(ResultStatus.Error, "Slayder tapılmadı!", new SliderDto
            {
                Slider = null,
                ResultStatus = ResultStatus.Error,
                Message = "Slayder tapılmadı!"
            });
        }
    }
}
