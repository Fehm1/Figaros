using Figaros.Entities.DTOs.PriceDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IPriceService
    {
        Task<IDataResult<PriceDto>> Get(int PriceId);
        Task<IDataResult<PriceUpdateDto>> GetUpdateDto(int PriceId);
        Task<IDataResult<PriceListDto>> GetAll();
        Task<IDataResult<PriceListDto>> GetAllByNonDeleted();
        Task<IDataResult<PriceListDto>> GetAllByDeleted();
        Task<IDataResult<PriceDto>> Add(PricePostDto PricePostDto);
        Task<IDataResult<PriceDto>> Update(PriceUpdateDto PriceUpdateDto);
        Task<IDataResult<PriceDto>> Restore(int PriceId);
        Task<IDataResult<PriceDto>> Delete(int PriceId);
        Task<IDataResult<PriceDto>> HardDelete(int PriceId);
    }
}
