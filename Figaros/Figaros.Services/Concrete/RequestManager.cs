using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Entities.DTOs.RequestDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Extentions;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace Figaros.Services.Concrete
{
    public class RequestManager : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public RequestManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IDataResult<RequestDto>> Add(RequestPostDto RequestPostDto)
        {
            if (RequestPostDto != null)
            {
                var request = _mapper.Map<Request>(RequestPostDto);

                if (RequestPostDto.CV != null)
                {
                    if (!RequestPostDto.CV.IsFileContent())
                    {
                        return new DataResult<RequestDto>(ResultStatus.Error, "Şəkil, pdf və ya word formatı daxil edin!", new RequestDto
                        {
                            Request = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil, pdf və ya word formatı daxil edin!"
                        });
                    }

                    if (!RequestPostDto.CV.IsValidFileLength())
                    {
                        return new DataResult<RequestDto>(ResultStatus.Error, "Faylın həcmi böyükdür!", new RequestDto
                        {
                            Request = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Faylın həcmi böyükdür!"
                        });
                    }

                    string newCV = RequestPostDto.CV.SaveImage(_env.WebRootPath, "uploads/Requests");
                    request.CV = newCV;
                }
                else
                {
                    return new DataResult<RequestDto>(ResultStatus.Error, "Fayl daxil edin!", new RequestDto
                    {
                        Request = null,
                        ResultStatus = ResultStatus.Error,
                        Message = "Fayl daxil edin!"
                    });
                }


                var addedRequest = await _unitOfWork.Requests.AddAsync(request);
                await _unitOfWork.SaveAsync();
                RequestDto requestDto = _mapper.Map<RequestDto>(request);

                return new DataResult<RequestDto>(ResultStatus.Success, "Müraciət uğurla əlavə olundu!", new RequestDto
                {
                    Request = addedRequest,
                    ResultStatus = ResultStatus.Success,
                    Message = "Müraciət uğurla əlavə olundu!"
                });
            }

            return new DataResult<RequestDto>(ResultStatus.Error, "Müraciət əlavə olunmadı!", new RequestDto
            {
                Request = null,
                ResultStatus = ResultStatus.Error,
                Message = "Müraciət əlavə olunmadı!"
            });
        }

        public async Task<IDataResult<RequestDto>> Delete(int RequestId)
        {
            var request = await _unitOfWork.Requests.GetAsync(x => x.Id == RequestId);

            if (request != null)
            {
                request.IsDeleted = true;
                var deletedRequest = await _unitOfWork.Requests.UpdateAsync(request);
                await _unitOfWork.SaveAsync();

                return new DataResult<RequestDto>(ResultStatus.Success, "Müraciət uğurla silindi!", new RequestDto
                {
                    Request = deletedRequest,
                    ResultStatus = ResultStatus.Success,
                    Message = "Müraciət uğurla silindi!"
                });
            }

            return new DataResult<RequestDto>(ResultStatus.Error, "Müraciət tapılmadı!", new RequestDto
            {
                Request = null,
                ResultStatus = ResultStatus.Error,
                Message = "Müraciət tapılmadı!"
            });
        }

        public async Task<IDataResult<RequestDto>> Get(int RequestId)
        {
            var request = await _unitOfWork.Requests.GetAsync(x => x.Id == RequestId);

            if (request != null)
            {
                return new DataResult<RequestDto>(ResultStatus.Success, new RequestDto
                {
                    Request = request,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<RequestDto>(ResultStatus.Error, "Müraciət tapılmadı!", new RequestDto
            {
                Request = null,
                ResultStatus = ResultStatus.Error,
                Message = "Müraciət tapılmadı!"
            });
        }

        public async Task<IDataResult<RequestListDto>> GetAll()
        {
            var requests = await _unitOfWork.Requests.GetAllAsync();

            if (requests.Count >= 0)
            {
                return new DataResult<RequestListDto>(ResultStatus.Success, new RequestListDto
                {
                    Requests = requests,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<RequestListDto>(ResultStatus.Error, "Müraciətlar tapılmadı!", new RequestListDto
            {
                Requests = null,
                ResultStatus = ResultStatus.Error,
                Message = "Müraciətlar tapılmadı!"
            });
        }

        public async Task<IDataResult<RequestListDto>> GetAllByDeleted()
        {
            var requests = await _unitOfWork.Requests.GetAllAsync(x => x.IsDeleted);

            if (requests.Count >= 0)
            {
                return new DataResult<RequestListDto>(ResultStatus.Success, new RequestListDto
                {
                    Requests = requests,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<RequestListDto>(ResultStatus.Error, "Müraciətlar tapılmadı!", new RequestListDto
            {
                Requests = null,
                ResultStatus = ResultStatus.Error,
                Message = "Müraciətlar tapılmadı!"
            });
        }

        public async Task<IDataResult<RequestListDto>> GetAllByNonDeleted()
        {
            var requests = await _unitOfWork.Requests.GetAllAsync(x => !x.IsDeleted);

            if (requests.Count >= 0)
            {
                return new DataResult<RequestListDto>(ResultStatus.Success, new RequestListDto
                {
                    Requests = requests,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<RequestListDto>(ResultStatus.Error, "Müraciətlar tapılmadı!", new RequestListDto
            {
                Requests = null,
                ResultStatus = ResultStatus.Error,
                Message = "Müraciətlar tapılmadı!"
            });
        }

        public async Task<IDataResult<RequestDto>> HardDelete(int RequestId)
        {
            var request = await _unitOfWork.Requests.GetAsync(x => x.Id == RequestId);

            if (request != null)
            {
                var deletedRequest = await _unitOfWork.Requests.DeleteAsync(request);
                request.CV.DeleteFile(_env.WebRootPath, "uploads/Requests");
                await _unitOfWork.SaveAsync();

                return new DataResult<RequestDto>(ResultStatus.Success, "Müraciət uğurla silindi!", new RequestDto
                {
                    Request = deletedRequest,
                    ResultStatus = ResultStatus.Success,
                    Message = "Müraciət uğurla silindi!"
                });
            }

            return new DataResult<RequestDto>(ResultStatus.Error, "Müraciət tapılmadı!", new RequestDto
            {
                Request = null,
                ResultStatus = ResultStatus.Error,
                Message = "Müraciət tapılmadı!"
            });
        }

        public async Task<IDataResult<RequestDto>> Readed(int RequestId)
        {
            var request = await _unitOfWork.Requests.GetAsync(x => x.Id == RequestId);

            if (request != null)
            {
                request.IsActive = false;
                var deletedRequest = await _unitOfWork.Requests.UpdateAsync(request);
                await _unitOfWork.SaveAsync();

                return new DataResult<RequestDto>(ResultStatus.Success, "Müraciət uğurla silindi!", new RequestDto
                {
                    Request = deletedRequest,
                    ResultStatus = ResultStatus.Success,
                    Message = "Müraciət uğurla silindi!"
                });
            }

            return new DataResult<RequestDto>(ResultStatus.Error, "Müraciət tapılmadı!", new RequestDto
            {
                Request = null,
                ResultStatus = ResultStatus.Error,
                Message = "Müraciət tapılmadı!"
            });
        }

        public async Task<IDataResult<RequestDto>> Restore(int RequestId)
        {
            var request = await _unitOfWork.Requests.GetAsync(x => x.Id == RequestId);

            if (request != null)
            {
                request.IsDeleted = false;
                var deletedRequest = await _unitOfWork.Requests.UpdateAsync(request);
                await _unitOfWork.SaveAsync();

                return new DataResult<RequestDto>(ResultStatus.Success, "Müraciət uğurla geri qaytarıldı!", new RequestDto
                {
                    Request = deletedRequest,
                    ResultStatus = ResultStatus.Success,
                    Message = "Müraciət uğurla geri qaytarıldı!"
                });
            }

            return new DataResult<RequestDto>(ResultStatus.Error, "Müraciət tapılmadı!", new RequestDto
            {
                Request = null,
                ResultStatus = ResultStatus.Error,
                Message = "Müraciət tapılmadı!"
            });
        }
    }
}
