using Figaros.Entities.DTOs.ProductDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<ProductDto>> Get(int ProductId);
        Task<IDataResult<ProductListDto>> GetAll();
        Task<IDataResult<ProductListDto>> GetAllByNonDeleted();
        Task<IDataResult<ProductListDto>> GetAllByDeleted();
        Task<IDataResult<ProductDto>> Add(ProductPostDto ProductPostDto);
        Task<IDataResult<ProductDto>> Update(ProductUpdateDto ProductUpdateDto);
        Task<IDataResult<ProductDto>> Restore(int ProductId);
        Task<IDataResult<ProductDto>> Delete(int ProductId);
        Task<IDataResult<ProductDto>> HardDelete(int ProductId);
    }
}
