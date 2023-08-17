using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Entities.DTOs.FAQDtos;
using Figaros.Entities.DTOs.PriceDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;

namespace Figaros.Services.Concrete
{
    public class PriceManager : IPriceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PriceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<PriceDto>> Add(PricePostDto PricePostDto)
        {
            if (PricePostDto != null)
            {
                var price = _mapper.Map<Price>(PricePostDto);

                var addedPrice = await _unitOfWork.Prices.AddAsync(price);
                await _unitOfWork.SaveAsync();
                PriceDto priceDto = _mapper.Map<PriceDto>(price);
                return new DataResult<PriceDto>(ResultStatus.Success, "Qiymət uğurla əlavə edildi!", new PriceDto
                {
                    Price = addedPrice,
                    ResultStatus = ResultStatus.Success,
                    Message = "Qiymət uğurla əlavə edildi!"
                });
            }

            return new DataResult<PriceDto>(ResultStatus.Error, "Qiymət əlavə edilmədi!", new PriceDto
            {
                Price = null,
                ResultStatus = ResultStatus.Error,
                Message = "Qiymət əlavə edilmədi!"
            });
        }

        public async Task<IDataResult<PriceDto>> Delete(int PriceId)
        {
            var price = await _unitOfWork.Prices.GetAsync(x => x.Id == PriceId);

            if (price != null)
            {
                price.IsDeleted = true;
                var deletedPrice = await _unitOfWork.Prices.UpdateAsync(price);
                await _unitOfWork.SaveAsync();

                return new DataResult<PriceDto>(ResultStatus.Success, "Qiymət uğurla silindi!", new PriceDto
                {
                    Price = deletedPrice,
                    ResultStatus = ResultStatus.Success,
                    Message = "Qiymət uğurla silindi!"
                });
            }

            return new DataResult<PriceDto>(ResultStatus.Error, "Qiymət tapılmadı!", new PriceDto
            {
                Price = null,
                ResultStatus = ResultStatus.Error,
                Message = "Qiymət tapılmadı!"
            });
        }

        public async Task<IDataResult<PriceDto>> Get(int PriceId)
        {
            var price = await _unitOfWork.Prices.GetAsync(x => x.Id == PriceId);

            if (price != null)
            {
                return new DataResult<PriceDto>(ResultStatus.Success, new PriceDto
                {
                    Price = price,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PriceDto>(ResultStatus.Error, "Qiymət tapılmadı!", new PriceDto
            {
                Price = null,
                ResultStatus = ResultStatus.Error,
                Message = "Qiymət tapılmadı!"
            });
        }

        public async Task<IDataResult<PriceListDto>> GetAll()
        {
            var prices = await _unitOfWork.Prices.GetAllAsync();

            if (prices.Count >= 0)
            {
                return new DataResult<PriceListDto>(ResultStatus.Success, new PriceListDto
                {
                    Prices = prices,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<PriceListDto>(ResultStatus.Error, "Qiymətlər tapılmadı!", new PriceListDto
            {
                Prices = null,
                ResultStatus = ResultStatus.Error,
                Message = "Qiymətlər tapılmadı!"
            });
        }

        public async Task<IDataResult<PriceListDto>> GetAllByDeleted()
        {
            var prices = await _unitOfWork.Prices.GetAllAsync(x => x.IsDeleted);

            if (prices.Count >= 0)
            {
                return new DataResult<PriceListDto>(ResultStatus.Success, new PriceListDto
                {
                    Prices = prices,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<PriceListDto>(ResultStatus.Error, "Qiymətlər tapılmadı!", new PriceListDto
            {
                Prices = null,
                ResultStatus = ResultStatus.Error,
                Message = "Qiymətlər tapılmadı!"
            });
        }

        public async Task<IDataResult<PriceListDto>> GetAllByNonDeleted()
        {
            var prices = await _unitOfWork.Prices.GetAllAsync(x => !x.IsDeleted);

            if (prices.Count >= 0)
            {
                return new DataResult<PriceListDto>(ResultStatus.Success, new PriceListDto
                {
                    Prices = prices,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<PriceListDto>(ResultStatus.Error, "Qiymətlər tapılmadı!", new PriceListDto
            {
                Prices = null,
                ResultStatus = ResultStatus.Error,
                Message = "Qiymətlər tapılmadı!"
            });
        }

        public async Task<IDataResult<PriceUpdateDto>> GetUpdateDto(int PriceId)
        {
            var result = await _unitOfWork.Prices.AnyAsync(c => c.Id == PriceId);
            if (result)
            {
                var price = await _unitOfWork.Prices.GetAsync(c => c.Id == PriceId);
                PriceUpdateDto priceUpdateDto = _mapper.Map<PriceUpdateDto>(price);
                return new DataResult<PriceUpdateDto>(ResultStatus.Success, priceUpdateDto);
            }
            else
            {
                return new DataResult<PriceUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<PriceDto>> HardDelete(int PriceId)
        {
            var price = await _unitOfWork.Prices.GetAsync(x => x.Id == PriceId);

            if (price != null)
            {
                var deletedPrice = await _unitOfWork.Prices.DeleteAsync(price);
                await _unitOfWork.SaveAsync();

                return new DataResult<PriceDto>(ResultStatus.Success, "Qiymət uğurla silindi!", new PriceDto
                {
                    Price = deletedPrice,
                    ResultStatus = ResultStatus.Success,
                    Message = "Qiymət uğurla silindi!"
                });
            }

            return new DataResult<PriceDto>(ResultStatus.Error, "Qiymət tapılmadı!", new PriceDto
            {
                Price = null,
                ResultStatus = ResultStatus.Error,
                Message = "Qiymət tapılmadı!"
            });
        }

        public async Task<IDataResult<PriceDto>> Restore(int PriceId)
        {
            var price = await _unitOfWork.Prices.GetAsync(x => x.Id == PriceId);

            if (price != null)
            {
                price.IsDeleted = false;
                var deletedPrice = await _unitOfWork.Prices.UpdateAsync(price);
                await _unitOfWork.SaveAsync();

                return new DataResult<PriceDto>(ResultStatus.Success, "Qiymət uğurla geri qaytarıldı!", new PriceDto
                {
                    Price = deletedPrice,
                    ResultStatus = ResultStatus.Success,
                    Message = "Qiymət uğurla geri qaytarıldı!"
                });
            }

            return new DataResult<PriceDto>(ResultStatus.Error, "Qiymət tapılmadı!", new PriceDto
            {
                Price = null,
                ResultStatus = ResultStatus.Error,
                Message = "Qiymət tapılmadı!"
            });
        }

        public async Task<IDataResult<PriceDto>> Update(PriceUpdateDto PriceUpdateDto)
        {
            var price = await _unitOfWork.Prices.GetAsync(x => x.Id == PriceUpdateDto.Id);

            if (price != null)
            {
                price.ServicePrice = PriceUpdateDto.ServicePrice;
                price.Service = PriceUpdateDto.Service;
                price.Description = PriceUpdateDto.Description;
                price.ModifiedDate = DateTime.Now;

                var updatedPrice = await _unitOfWork.Prices.UpdateAsync(price);
                await _unitOfWork.SaveAsync();

                return new DataResult<PriceDto>(ResultStatus.Success, "Qiymət uğurla yeniləndi!", new PriceDto
                {
                    Price = updatedPrice,
                    ResultStatus = ResultStatus.Success,
                    Message = "Qiymət uğurla yeniləndi!"
                });
            }

            return new DataResult<PriceDto>(ResultStatus.Error, "Qiymət tapılmadı!", new PriceDto
            {
                Price = null,
                ResultStatus = ResultStatus.Error,
                Message = "Qiymət tapılmadı!"
            });
        }
    }
}
