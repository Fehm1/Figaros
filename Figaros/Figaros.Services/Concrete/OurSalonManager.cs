using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Entities.DTOs.OurSalonDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Extentions;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace Figaros.Services.Concrete
{
    public class OurSalonManager : IOurSalonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public OurSalonManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IDataResult<OurSalonDto>> Get(int OurSalonId)
        {
            var ourSalon = await _unitOfWork.OurSalon.GetAsync(x => x.Id == OurSalonId);

            if (ourSalon != null)
            {
                return new DataResult<OurSalonDto>(ResultStatus.Success, new OurSalonDto
                {
                    OurSalon = ourSalon,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<OurSalonDto>(ResultStatus.Error, "Məlumat tapılmadı!", new OurSalonDto
            {
                OurSalon = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məlumat tapılmadı!"
            });
        }

        public async Task<IDataResult<OurSalonListDto>> GetAll()
        {
            var ourSalons = await _unitOfWork.OurSalon.GetAllAsync();

            if (ourSalons.Count >= 0)
            {
                return new DataResult<OurSalonListDto>(ResultStatus.Success, new OurSalonListDto
                {
                    OurSalons = ourSalons,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<OurSalonListDto>(ResultStatus.Error, "Məlumatlar tapılmadı!", new OurSalonListDto
            {
                OurSalons = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məlumatlar tapılmadı!"
            });
        }

        public async Task<IDataResult<OurSalonUpdateDto>> GetUpdateDto(int OurSalonId)
        {
            var result = await _unitOfWork.OurSalon.AnyAsync(c => c.Id == OurSalonId);
            if (result)
            {
                var ourSalon = await _unitOfWork.OurSalon.GetAsync(c => c.Id == OurSalonId);
                OurSalonUpdateDto bookingUpdateDto = _mapper.Map<OurSalonUpdateDto>(ourSalon);
                return new DataResult<OurSalonUpdateDto>(ResultStatus.Success, bookingUpdateDto);
            }
            else
            {
                return new DataResult<OurSalonUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<OurSalonDto>> Update(OurSalonUpdateDto OurSalonUpdateDto)
        {
            var ourSalon = await _unitOfWork.OurSalon.GetAsync(x => x.Id == OurSalonUpdateDto.Id);

            if (ourSalon != null)
            {
                if (OurSalonUpdateDto.ImageFile != null)
                {
                    if (!OurSalonUpdateDto.ImageFile.IsImageContent())
                    {
                        return new DataResult<OurSalonDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new OurSalonDto
                        {
                            OurSalon = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!OurSalonUpdateDto.ImageFile.IsValidImageLength())
                    {
                        return new DataResult<OurSalonDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new OurSalonDto
                        {
                            OurSalon = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = OurSalonUpdateDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/OurSalon");
                    OurSalonUpdateDto.ImageString.DeleteImage(_env.WebRootPath, "uploads/OurSalon");

                    ourSalon.ImageString = newImage;
                }

                ourSalon.Title = OurSalonUpdateDto.Title;
                ourSalon.LittleTitle = OurSalonUpdateDto.LittleTitle;
                ourSalon.Description = OurSalonUpdateDto.Description;
                ourSalon.RedirectUrl = OurSalonUpdateDto.RedirectUrl;
                ourSalon.IsActive = OurSalonUpdateDto.IsActive;
                ourSalon.ModifiedDate = DateTime.Now;

                var updatedOurSalon = await _unitOfWork.OurSalon.UpdateAsync(ourSalon);
                await _unitOfWork.SaveAsync();

                return new DataResult<OurSalonDto>(ResultStatus.Success, "Məlumatlar uğurla yeniləndi!", new OurSalonDto
                {
                    OurSalon = updatedOurSalon,
                    ResultStatus = ResultStatus.Error,
                    Message = "Məlumatlar uğurla yeniləndi!"
                });
            }

            return new DataResult<OurSalonDto>(ResultStatus.Error, "Məlumatlar tapılmadı!", new OurSalonDto
            {
                OurSalon = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məlumatlar tapılmadı!"
            });
        }
    }
}
