using Figaros.Entities.DTOs.ImageDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IImageService
    {
        Task<IDataResult<ImageDto>> Get(int ImageId);
        Task<IDataResult<ImageListDto>> GetAll();
        Task<IDataResult<ImageListDto>> GetAllByNonDeleted();
        Task<IDataResult<ImageListDto>> GetAllByDeleted();
        Task<IDataResult<ImageDto>> Update(ImageUpdateDto ImageUpdateDto);
        Task<IDataResult<ImageDto>> Restore(int ImageId);
        Task<IDataResult<ImageDto>> Delete(int ImageId);
        Task<IDataResult<ImageDto>> HardDelete(int ImageId);
    }
}
