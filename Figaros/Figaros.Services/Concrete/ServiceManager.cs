using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Entities.DTOs.ServiceDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Extentions;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace Figaros.Services.Concrete
{
    public class ServiceManager : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IDataResult<ServiceDto>> Add(ServicePostDto ServicePostDto)
        {
            if (ServicePostDto != null)
            {
                var service = _mapper.Map<Service>(ServicePostDto);

                if (ServicePostDto.ImageFile != null)
                {
                    if (!ServicePostDto.ImageFile.IsImageContent())
                    {
                        return new DataResult<ServiceDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new ServiceDto
                        {
                            Service = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!ServicePostDto.ImageFile.IsValidImageLength())
                    {
                        return new DataResult<ServiceDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new ServiceDto
                        {
                            Service = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = ServicePostDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Services");
                    service.ImageString = newImage;
                }
                else
                {
                    return new DataResult<ServiceDto>(ResultStatus.Error, "Şəkil daxil edin!", new ServiceDto
                    {
                        Service = null,
                        ResultStatus = ResultStatus.Error,
                        Message = "Şəkil daxil edin!"
                    });
                }


                var addedService = await _unitOfWork.Services.AddAsync(service);
                await _unitOfWork.SaveAsync();
                ServiceDto serviceDto = _mapper.Map<ServiceDto>(service);

                return new DataResult<ServiceDto>(ResultStatus.Success, "Xidmət uğurla əlavə olundu!", new ServiceDto
                {
                    Service = addedService,
                    ResultStatus = ResultStatus.Success,
                    Message = "Xidmət uğurla əlavə olundu!"
                });
            }

            return new DataResult<ServiceDto>(ResultStatus.Error, "Xidmət əlavə olunmadı!", new ServiceDto
            {
                Service = null,
                ResultStatus = ResultStatus.Error,
                Message = "Xidmət əlavə olunmadı!"
            });
        }

        public async Task<IDataResult<ServiceDto>> Delete(int ServiceId)
        {
            var serive = await _unitOfWork.Services.GetAsync(x => x.Id == ServiceId);

            if (serive != null)
            {
                serive.IsDeleted = true;
                var deletedService = await _unitOfWork.Services.UpdateAsync(serive);
                await _unitOfWork.SaveAsync();

                return new DataResult<ServiceDto>(ResultStatus.Success, "Xidmət uğurla silindi!", new ServiceDto
                {
                    Service = deletedService,
                    ResultStatus = ResultStatus.Success,
                    Message = "Xidmət uğurla silindi!"
                });
            }

            return new DataResult<ServiceDto>(ResultStatus.Error, "Xidmət tapılmadı!", new ServiceDto
            {
                Service = null,
                ResultStatus = ResultStatus.Error,
                Message = "Xidmət tapılmadı!"
            });
        }

        public async Task<IDataResult<ServiceDto>> Get(int ServiceId)
        {
            var serive = await _unitOfWork.Services.GetAsync(x => x.Id == ServiceId);

            if (serive != null)
            {
                return new DataResult<ServiceDto>(ResultStatus.Success, new ServiceDto
                {
                    Service = serive,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ServiceDto>(ResultStatus.Error, "Xidmət tapılmadı!", new ServiceDto
            {
                Service = null,
                ResultStatus = ResultStatus.Error,
                Message = "Xidmət tapılmadı!"
            });
        }

        public async Task<IDataResult<ServiceListDto>> GetAll()
        {
            var services = await _unitOfWork.Services.GetAllAsync();

            if (services.Count >= 0)
            {
                return new DataResult<ServiceListDto>(ResultStatus.Success, new ServiceListDto
                {
                    Services = services,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ServiceListDto>(ResultStatus.Error, "Xidmətlar tapılmadı!", new ServiceListDto
            {
                Services = null,
                ResultStatus = ResultStatus.Error,
                Message = "Xidmətlar tapılmadı!"
            });
        }

        public async Task<IDataResult<ServiceListDto>> GetAllByDeleted()
        {
            var services = await _unitOfWork.Services.GetAllAsync(x => x.IsDeleted);

            if (services.Count >= 0)
            {
                return new DataResult<ServiceListDto>(ResultStatus.Success, new ServiceListDto
                {
                    Services = services,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ServiceListDto>(ResultStatus.Error, "Xidmətlar tapılmadı!", new ServiceListDto
            {
                Services = null,
                ResultStatus = ResultStatus.Error,
                Message = "Xidmətlar tapılmadı!"
            });
        }

        public async Task<IDataResult<ServiceListDto>> GetAllByNonDeleted()
        {
            var services = await _unitOfWork.Services.GetAllAsync(x => !x.IsDeleted);

            if (services.Count >= 0)
            {
                return new DataResult<ServiceListDto>(ResultStatus.Success, new ServiceListDto
                {
                    Services = services,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ServiceListDto>(ResultStatus.Error, "Xidmətlar tapılmadı!", new ServiceListDto
            {
                Services = null,
                ResultStatus = ResultStatus.Error,
                Message = "Xidmətlar tapılmadı!"
            });
        }

        public async Task<IDataResult<ServiceUpdateDto>> GetUpdateDto(int ServiceId)
        {
            var result = await _unitOfWork.Services.AnyAsync(c => c.Id == ServiceId);
            if (result)
            {
                var service = await _unitOfWork.Services.GetAsync(c => c.Id == ServiceId);
                ServiceUpdateDto serviceUpdateDto = _mapper.Map<ServiceUpdateDto>(service);
                return new DataResult<ServiceUpdateDto>(ResultStatus.Success, serviceUpdateDto);
            }
            else
            {
                return new DataResult<ServiceUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<ServiceDto>> HardDelete(int ServiceId)
        {
            var serive = await _unitOfWork.Services.GetAsync(x => x.Id == ServiceId);

            if (serive != null)
            {
                var deletedService = await _unitOfWork.Services.DeleteAsync(serive);
                await _unitOfWork.SaveAsync();

                return new DataResult<ServiceDto>(ResultStatus.Success, "Xidmət uğurla silindi!", new ServiceDto
                {
                    Service = deletedService,
                    ResultStatus = ResultStatus.Success,
                    Message = "Xidmət uğurla silindi!"
                });
            }

            return new DataResult<ServiceDto>(ResultStatus.Error, "Xidmət tapılmadı!", new ServiceDto
            {
                Service = null,
                ResultStatus = ResultStatus.Error,
                Message = "Xidmət tapılmadı!"
            });
        }

        public async Task<IDataResult<ServiceDto>> Restore(int ServiceId)
        {
            var service = await _unitOfWork.Services.GetAsync(x => x.Id == ServiceId);

            if (service != null)
            {
                service.IsDeleted = false;
                var deletedService = await _unitOfWork.Services.UpdateAsync(service);
                await _unitOfWork.SaveAsync();

                return new DataResult<ServiceDto>(ResultStatus.Success, "Xidmət uğurla geri qaytarıldı!", new ServiceDto
                {
                    Service = deletedService,
                    ResultStatus = ResultStatus.Success,
                    Message = "Xidmət uğurla geri qaytarıldı!"
                });
            }

            return new DataResult<ServiceDto>(ResultStatus.Error, "Xidmət tapılmadı!", new ServiceDto
            {
                Service = null,
                ResultStatus = ResultStatus.Error,
                Message = "Xidmət tapılmadı!"
            });
        }

        public async Task<IDataResult<ServiceDto>> Update(ServiceUpdateDto ServiceUpdateDto)
        {
            var service = await _unitOfWork.Services.GetAsync(x => x.Id == ServiceUpdateDto.Id);

            if (service != null)
            {
                if (ServiceUpdateDto.ImageFile != null)
                {
                    if (!ServiceUpdateDto.ImageFile.IsImageContent())
                    {
                        return new DataResult<ServiceDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new ServiceDto
                        {
                            Service = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!ServiceUpdateDto.ImageFile.IsValidImageLength())
                    {
                        return new DataResult<ServiceDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new ServiceDto
                        {
                            Service = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = ServiceUpdateDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Employees");
                    service.ImageString.DeleteImage(_env.WebRootPath, "uploads/Employees");

                    service.ImageString = newImage;
                }

                service.Title = ServiceUpdateDto.Title;
                service.Description = ServiceUpdateDto.Description;
                service.IsPoster = ServiceUpdateDto.IsPoster;
                service.IsActive = ServiceUpdateDto.IsActive;
                service.ModifiedDate = DateTime.Now;

                var updatedService = await _unitOfWork.Services.UpdateAsync(service);
                await _unitOfWork.SaveAsync();

                return new DataResult<ServiceDto>(ResultStatus.Success, "Xidmət uğurla yeniləndi!", new ServiceDto
                {
                    Service = updatedService,
                    ResultStatus = ResultStatus.Error,
                    Message = "Xidmət uğurla yeniləndi!"
                });
            }

            return new DataResult<ServiceDto>(ResultStatus.Error, "Xidmət tapılmadı!", new ServiceDto
            {
                Service = null,
                ResultStatus = ResultStatus.Error,
                Message = "Xidmət tapılmadı!"
            });
        }
    }
}
