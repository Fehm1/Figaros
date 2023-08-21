using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.PriceDtos;
using Figaros.Entities.DTOs.TimeDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;

namespace Figaros.Services.Concrete
{
    public class TimeManager : ITimeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TimeManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<TimeDto>> Add(TimePostDto TimePostDto)
        {
            if (TimePostDto != null)
            {
                var time = _mapper.Map<Time>(TimePostDto);

                var addedTime = await _unitOfWork.Times.AddAsync(time);
                await _unitOfWork.SaveAsync();
                TimeDto timeDto = _mapper.Map<TimeDto>(time);
                return new DataResult<TimeDto>(ResultStatus.Success, "Vaxt uğurla əlavə edildi!", new TimeDto
                {
                    Time = addedTime,
                    ResultStatus = ResultStatus.Success,
                    Message = "Vaxt uğurla əlavə edildi!"
                });
            }

            return new DataResult<TimeDto>(ResultStatus.Error, "Vaxt əlavə edilmədi!", new TimeDto
            {
                Time = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vaxt əlavə edilmədi!"
            });
        }

        public async Task<IDataResult<TimeDto>> Delete(int TimeId)
        {
            var time = await _unitOfWork.Times.GetAsync(x => x.Id == TimeId);

            if (time != null)
            {
                time.IsDeleted = true;
                var deletedTime = await _unitOfWork.Times.UpdateAsync(time);
                await _unitOfWork.SaveAsync();

                return new DataResult<TimeDto>(ResultStatus.Success, "Vaxt uğurla silindi!", new TimeDto
                {
                    Time = deletedTime,
                    ResultStatus = ResultStatus.Success,
                    Message = "Vaxt uğurla silindi!"
                });
            }

            return new DataResult<TimeDto>(ResultStatus.Error, "Vaxt tapılmadı!", new TimeDto
            {
                Time = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vaxt tapılmadı!"
            });
        }

        public async Task<IDataResult<TimeDto>> Get(int TimeId)
        {
            var time = await _unitOfWork.Times.GetAsync(x => x.Id == TimeId);

            if (time != null)
            {
                return new DataResult<TimeDto>(ResultStatus.Success, new TimeDto
                {
                    Time = time,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<TimeDto>(ResultStatus.Error, "Vaxt tapılmadı!", new TimeDto
            {
                Time = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vaxt tapılmadı!"
            });
        }

        public async Task<IDataResult<TimeListDto>> GetAll()
        {
            var times = await _unitOfWork.Times.GetAllAsync();

            if (times.Count >= 0)
            {
                return new DataResult<TimeListDto>(ResultStatus.Success, new TimeListDto
                {
                    Times = times,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<TimeListDto>(ResultStatus.Error, "Vaxtlər tapılmadı!", new TimeListDto
            {
                Times = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vaxtlər tapılmadı!"
            });
        }

        public async Task<IDataResult<TimeListDto>> GetAllByDeleted()
        {
            var times = await _unitOfWork.Times.GetAllAsync(x => x.IsDeleted);

            if (times.Count >= 0)
            {
                return new DataResult<TimeListDto>(ResultStatus.Success, new TimeListDto
                {
                    Times = times,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<TimeListDto>(ResultStatus.Error, "Vaxtlər tapılmadı!", new TimeListDto
            {
                Times = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vaxtlər tapılmadı!"
            });
        }

        public async Task<IDataResult<TimeListDto>> GetAllByNonDeleted()
        {
            var times = await _unitOfWork.Times.GetAllAsync(x => !x.IsDeleted);

            if (times.Count >= 0)
            {
                return new DataResult<TimeListDto>(ResultStatus.Success, new TimeListDto
                {
                    Times = times,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<TimeListDto>(ResultStatus.Error, "Vaxtlər tapılmadı!", new TimeListDto
            {
                Times = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vaxtlər tapılmadı!"
            });
        }

        public async Task<IDataResult<TimeUpdateDto>> GetUpdateDto(int TimeId)
        {
            var result = await _unitOfWork.Times.AnyAsync(c => c.Id == TimeId);
            if (result)
            {
                var time = await _unitOfWork.Times.GetAsync(c => c.Id == TimeId);
                TimeUpdateDto timeUpdateDto = _mapper.Map<TimeUpdateDto>(time);
                return new DataResult<TimeUpdateDto>(ResultStatus.Success, timeUpdateDto);
            }
            else
            {
                return new DataResult<TimeUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<TimeDto>> HardDelete(int TimeId)
        {
            var time = await _unitOfWork.Times.GetAsync(x => x.Id == TimeId);

            if (time != null)
            {
                var deletedTime = await _unitOfWork.Times.DeleteAsync(time);
                await _unitOfWork.SaveAsync();

                return new DataResult<TimeDto>(ResultStatus.Success, "Vaxt uğurla silindi!", new TimeDto
                {
                    Time = deletedTime,
                    ResultStatus = ResultStatus.Success,
                    Message = "Vaxt uğurla silindi!"
                });
            }

            return new DataResult<TimeDto>(ResultStatus.Error, "Vaxt tapılmadı!", new TimeDto
            {
                Time = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vaxt tapılmadı!"
            });
        }

        public async Task<IDataResult<TimeDto>> Restore(int TimeId)
        {
            var time = await _unitOfWork.Times.GetAsync(x => x.Id == TimeId);

            if (time != null)
            {
                time.IsDeleted = false;
                var deletedTime = await _unitOfWork.Times.UpdateAsync(time);
                await _unitOfWork.SaveAsync();

                return new DataResult<TimeDto>(ResultStatus.Success, "Vaxt uğurla silindi!", new TimeDto
                {
                    Time = deletedTime,
                    ResultStatus = ResultStatus.Success,
                    Message = "Vaxt uğurla silindi!"
                });
            }

            return new DataResult<TimeDto>(ResultStatus.Error, "Vaxt tapılmadı!", new TimeDto
            {
                Time = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vaxt tapılmadı!"
            });
        }

        public async Task<IDataResult<TimeDto>> Update(TimeUpdateDto TimeUpdateDto)
        {
            var time = await _unitOfWork.Times.GetAsync(x => x.Id == TimeUpdateDto.Id);

            if (time != null)
            {
                time.Hour = TimeUpdateDto.Hour;
                time.IsActive = TimeUpdateDto.IsActive;
                time.ModifiedDate = DateTime.Now;

                var updatedTime = await _unitOfWork.Times.UpdateAsync(time);
                await _unitOfWork.SaveAsync();

                return new DataResult<TimeDto>(ResultStatus.Success, "Vaxt uğurla yeniləndi!", new TimeDto
                {
                    Time = updatedTime,
                    ResultStatus = ResultStatus.Success,
                    Message = "Vaxt uğurla yeniləndi!"
                });
            }

            return new DataResult<TimeDto>(ResultStatus.Error, "Vaxt tapılmadı!", new TimeDto
            {
                Time = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vaxt tapılmadı!"
            });
        }
    }
}
