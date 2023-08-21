using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Data.Concrete;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Entities.DTOs.ProductDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Extentions;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace Figaros.Services.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IDataResult<ProductDto>> Add(ProductPostDto ProductPostDto)
        {
            if (ProductPostDto != null)
            {
                var product = _mapper.Map<Product>(ProductPostDto);

                if (ProductPostDto.ImageFile != null)
                {
                    if (!ProductPostDto.ImageFile.IsImageContent())
                    {
                        return new DataResult<ProductDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new ProductDto
                        {
                            Product = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!ProductPostDto.ImageFile.IsValidImageLength())
                    {
                        return new DataResult<ProductDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new ProductDto
                        {
                            Product = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = ProductPostDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Products");
                    product.ImageString = newImage;
                }
                else
                {
                    return new DataResult<ProductDto>(ResultStatus.Error, "Şəkil daxil edin!", new ProductDto
                    {
                        Product = null,
                        ResultStatus = ResultStatus.Error,
                        Message = "Şəkil daxil edin!"
                    });
                }


                var addedProduct = await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveAsync();
                ProductDto productDto = _mapper.Map<ProductDto>(product);

                return new DataResult<ProductDto>(ResultStatus.Success, "Məhsul uğurla əlavə olundu!", new ProductDto
                {
                    Product = addedProduct,
                    ResultStatus = ResultStatus.Success,
                    Message = "Məhsul uğurla əlavə olundu!"
                });
            }

            return new DataResult<ProductDto>(ResultStatus.Error, "Məhsul əlavə olunmadı!", new ProductDto
            {
                Product = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məhsul əlavə olunmadı!"
            });
        }

        public async Task<IDataResult<ProductDto>> Delete(int ProductId)
        {
            var product = await _unitOfWork.Products.GetAsync(x => x.Id == ProductId);

            if (product != null)
            {
                product.IsDeleted = true;
                var deletedProduct = await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.SaveAsync();

                return new DataResult<ProductDto>(ResultStatus.Success, "Məhsul uğurla silindi!", new ProductDto
                {
                    Product = deletedProduct,
                    ResultStatus = ResultStatus.Success,
                    Message = "Məhsul uğurla silindi!"
                });
            }

            return new DataResult<ProductDto>(ResultStatus.Error, "Məhsul tapılmadı!", new ProductDto
            {
                Product = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məhsul tapılmadı!"
            });
        }

        public async Task<IDataResult<ProductDto>> Get(int ProductId)
        {
            var product = await _unitOfWork.Products.GetAsync(x => x.Id == ProductId);

            if (product != null)
            {
                return new DataResult<ProductDto>(ResultStatus.Success, new ProductDto
                {
                    Product = product,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProductDto>(ResultStatus.Error, "Məhsul tapılmadı!", new ProductDto
            {
                Product = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məhsul tapılmadı!"
            });
        }

        public async Task<IDataResult<ProductListDto>> GetAll()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            if (products.Count >= 0)
            {
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ProductListDto>(ResultStatus.Error, "Məhsullar tapılmadı!", new ProductListDto
            {
                Products = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məhsullar tapılmadı!"
            });
        }

        public async Task<IDataResult<ProductListDto>> GetAllByDeleted()
        {
            var products = await _unitOfWork.Products.GetAllAsync(x => x.IsDeleted);

            if (products.Count >= 0)
            {
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ProductListDto>(ResultStatus.Error, "Məhsullar tapılmadı!", new ProductListDto
            {
                Products = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məhsullar tapılmadı!"
            });
        }

        public async Task<IDataResult<ProductListDto>> GetAllByNonDeleted()
        {
            var products = await _unitOfWork.Products.GetAllAsync(x => !x.IsDeleted);

            if (products.Count >= 0)
            {
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ProductListDto>(ResultStatus.Error, "Məhsullar tapılmadı!", new ProductListDto
            {
                Products = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məhsullar tapılmadı!"
            });
        }

        public async Task<IDataResult<ProductUpdateDto>> GetUpdateDto(int ProductId)
        {
            var result = await _unitOfWork.Products.AnyAsync(c => c.Id == ProductId);
            if (result)
            {
                var product = await _unitOfWork.Employees.GetAsync(c => c.Id == ProductId);
                ProductUpdateDto productUpdateDto = _mapper.Map<ProductUpdateDto>(product);
                return new DataResult<ProductUpdateDto>(ResultStatus.Success, productUpdateDto);
            }
            else
            {
                return new DataResult<ProductUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<ProductDto>> HardDelete(int ProductId)
        {
            var product = await _unitOfWork.Products.GetAsync(x => x.Id == ProductId);

            if (product != null)
            {
                var deletedProduct = await _unitOfWork.Products.DeleteAsync(product);
                product.ImageString.DeleteImage(_env.WebRootPath, "uploads/Products");
                await _unitOfWork.SaveAsync();

                return new DataResult<ProductDto>(ResultStatus.Success, "Məhsul uğurla silindi!", new ProductDto
                {
                    Product = deletedProduct,
                    ResultStatus = ResultStatus.Success,
                    Message = "Məhsul uğurla silindi!"
                });
            }

            return new DataResult<ProductDto>(ResultStatus.Error, "Məhsul tapılmadı!", new ProductDto
            {
                Product = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məhsul tapılmadı!"
            });
        }

        public async Task<IDataResult<ProductDto>> Restore(int ProductId)
        {
            var product = await _unitOfWork.Products.GetAsync(x => x.Id == ProductId);

            if (product != null)
            {
                product.IsDeleted = false;
                var deletedProduct = await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.SaveAsync();

                return new DataResult<ProductDto>(ResultStatus.Success, "Məhsul uğurla geri qaytarıldı!", new ProductDto
                {
                    Product = deletedProduct,
                    ResultStatus = ResultStatus.Success,
                    Message = "Məhsul uğurla geri qaytarıldı!"
                });
            }

            return new DataResult<ProductDto>(ResultStatus.Error, "Məhsul tapılmadı!", new ProductDto
            {
                Product = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məhsul tapılmadı!"
            });
        }

        public async Task<IDataResult<ProductDto>> Update(ProductUpdateDto ProductUpdateDto)
        {
            var product = await _unitOfWork.Products.GetAsync(x => x.Id == ProductUpdateDto.Id);

            if (product != null)
            {
                if (ProductUpdateDto.ImageFile != null)
                {
                    if (!ProductUpdateDto.ImageFile.IsImageContent())
                    {
                        return new DataResult<ProductDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new ProductDto
                        {
                            Product = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!ProductUpdateDto.ImageFile.IsValidImageLength())
                    {
                        return new DataResult<ProductDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new ProductDto
                        {
                            Product = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = ProductUpdateDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Products");
                    product.ImageString.DeleteImage(_env.WebRootPath, "uploads/Products");

                    product.ImageString = newImage;
                }

                product.Title = ProductUpdateDto.Title;
                product.Description = ProductUpdateDto.Description;
                product.CostPrice = ProductUpdateDto.CostPrice;
                product.SalePrice = ProductUpdateDto.SalePrice;
                product.DiscountPercent = ProductUpdateDto.DiscountPercent;
                product.ProductAmount = ProductUpdateDto.ProductAmount;
                product.SaleCount = ProductUpdateDto.SaleCount;
                product.IsNew = ProductUpdateDto.IsNew;
                product.IsPopular = ProductUpdateDto.IsPopular;
                product.IsActive = ProductUpdateDto.IsActive;
                product.ModifiedDate = DateTime.Now;

                var updatedProduct = await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.SaveAsync();

                return new DataResult<ProductDto>(ResultStatus.Success, "Məhsul uğurla yeniləndi!", new ProductDto
                {
                    Product = updatedProduct,
                    ResultStatus = ResultStatus.Error,
                    Message = "Məhsul uğurla yeniləndi!"
                });
            }

            return new DataResult<ProductDto>(ResultStatus.Error, "Məhsul tapılmadı!", new ProductDto
            {
                Product = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məhsul tapılmadı!"
            });
        }
    }
}
