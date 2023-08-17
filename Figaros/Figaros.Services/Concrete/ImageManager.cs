using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Entities.DTOs.ImageDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Extentions;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace Figaros.Services.Concrete
{
    public class ImageManager : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ImageManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IDataResult<ImageDto>> Delete(int ImageId)
        {
            var image = await _unitOfWork.Images.GetAsync(x => x.Id == ImageId);

            if (image != null)
            {
                image.IsDeleted = true;
                var deletedImage = await _unitOfWork.Images.UpdateAsync(image);
                await _unitOfWork.SaveAsync();

                return new DataResult<ImageDto>(ResultStatus.Success, "Şəkil uğurla silindi!", new ImageDto
                {
                    Image = deletedImage,
                    ResultStatus = ResultStatus.Success,
                    Message = "Şəkil uğurla silindi!"
                });
            }

            return new DataResult<ImageDto>(ResultStatus.Error, "Şəkil tapılmadı!", new ImageDto
            {
                Image = null,
                ResultStatus = ResultStatus.Error,
                Message = "Şəkil tapılmadı!"
            });
        }

        public async Task<IDataResult<ImageDto>> Get(int ImageId)
        {
            var image = await _unitOfWork.Images.GetAsync(x => x.Id == ImageId);

            if (image != null)
            {
                return new DataResult<ImageDto>(ResultStatus.Success, new ImageDto
                {
                    Image = image,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ImageDto>(ResultStatus.Error, "Şəkil tapılmadı!", new ImageDto
            {
                Image = null,
                ResultStatus = ResultStatus.Error,
                Message = "Şəkil tapılmadı!"
            });
        }

        public async Task<IDataResult<ImageListDto>> GetAll()
        {
            var images = await _unitOfWork.Images.GetAllAsync();

            if (images.Count >= 0)
            {
                return new DataResult<ImageListDto>(ResultStatus.Success, new ImageListDto
                {
                    Images = images,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ImageListDto>(ResultStatus.Error, "Şəkil tapılmadı!", new ImageListDto
            {
                Images = null,
                ResultStatus = ResultStatus.Error,
                Message = "Şəkil tapılmadı!"
            });
        }

        public async Task<IDataResult<ImageListDto>> GetAllByDeleted()
        {
            var images = await _unitOfWork.Images.GetAllAsync(x => x.IsDeleted);

            if (images.Count >= 0)
            {
                return new DataResult<ImageListDto>(ResultStatus.Success, new ImageListDto
                {
                    Images = images,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ImageListDto>(ResultStatus.Error, "Şəkil tapılmadı!", new ImageListDto
            {
                Images = null,
                ResultStatus = ResultStatus.Error,
                Message = "Şəkil tapılmadı!"
            });
        }

        public async Task<IDataResult<ImageListDto>> GetAllByNonDeleted()
        {
            var images = await _unitOfWork.Images.GetAllAsync(x => !x.IsDeleted);

            if (images.Count >= 0)
            {
                return new DataResult<ImageListDto>(ResultStatus.Success, new ImageListDto
                {
                    Images = images,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ImageListDto>(ResultStatus.Error, "Şəkil tapılmadı!", new ImageListDto
            {
                Images = null,
                ResultStatus = ResultStatus.Error,
                Message = "Şəkil tapılmadı!"
            });
        }

        public async Task<IDataResult<ImageUpdateDto>> GetUpdateDto(int ImageId)
        {
            var result = await _unitOfWork.Images.AnyAsync(c => c.Id == ImageId);
            if (result)
            {
                var image = await _unitOfWork.Images.GetAsync(c => c.Id == ImageId);
                ImageUpdateDto FAQUpdateDto = _mapper.Map<ImageUpdateDto>(image);
                return new DataResult<ImageUpdateDto>(ResultStatus.Success, FAQUpdateDto);
            }
            else
            {
                return new DataResult<ImageUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<ImageDto>> HardDelete(int ImageId)
        {
            var image = await _unitOfWork.Images.GetAsync(x => x.Id == ImageId);

            if (image != null)
            {
                var deletedImage = await _unitOfWork.Images.DeleteAsync(image);
                image.ImageString.DeleteImage(_env.WebRootPath, "uploads/Images");
                await _unitOfWork.SaveAsync();

                return new DataResult<ImageDto>(ResultStatus.Success, "Şəkil uğurla silindi!", new ImageDto
                {
                    Image = deletedImage,
                    ResultStatus = ResultStatus.Success,
                    Message = "Şəkil uğurla silindi!"
                });
            }

            return new DataResult<ImageDto>(ResultStatus.Error, "Şəkil tapılmadı!", new ImageDto
            {
                Image = null,
                ResultStatus = ResultStatus.Error,
                Message = "Şəkil tapılmadı!"
            });
        }

        public async Task<IDataResult<ImageDto>> Restore(int ImageId)
        {
            var image = await _unitOfWork.Images.GetAsync(x => x.Id == ImageId);

            if (image != null)
            {
                image.IsDeleted = false;
                var deletedImage = await _unitOfWork.Images.UpdateAsync(image);
                await _unitOfWork.SaveAsync();

                return new DataResult<ImageDto>(ResultStatus.Success, "Şəkil uğurla geri qaytarıldı!", new ImageDto
                {
                    Image = deletedImage,
                    ResultStatus = ResultStatus.Success,
                    Message = "Şəkil uğurla geri qaytarıldı!"
                });
            }

            return new DataResult<ImageDto>(ResultStatus.Error, "Şəkil tapılmadı!", new ImageDto
            {
                Image = null,
                ResultStatus = ResultStatus.Error,
                Message = "Şəkil tapılmadı!"
            });
        }

        public async Task<IDataResult<ImageDto>> Update(ImageUpdateDto ImageUpdateDto)
        {
            var image = await _unitOfWork.Images.GetAsync(x => x.Id == ImageUpdateDto.Id);

            if (image != null)
            {
                if (ImageUpdateDto.ImageFile != null)
                {
                    if (!ImageUpdateDto.ImageFile.IsImageContent())
                    {
                        return new DataResult<ImageDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new ImageDto
                        {
                            Image = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!ImageUpdateDto.ImageFile.IsValidImageLength())
                    {
                        return new DataResult<ImageDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new ImageDto
                        {
                            Image = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = ImageUpdateDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Images");
                    image.ImageString.DeleteImage(_env.WebRootPath, "uploads/Images");

                    image.ImageString = newImage;
                }

                image.ModifiedDate = DateTime.Now;

                var updatedImage = await _unitOfWork.Images.UpdateAsync(image);
                await _unitOfWork.SaveAsync();

                return new DataResult<ImageDto>(ResultStatus.Success, "Şəkil uğurla yeniləndi!", new ImageDto
                {
                    Image = updatedImage,
                    ResultStatus = ResultStatus.Error,
                    Message = "Şəkil uğurla yeniləndi!"
                });
            }

            return new DataResult<ImageDto>(ResultStatus.Error, "Şəkil tapılmadı!", new ImageDto
            {
                Image = null,
                ResultStatus = ResultStatus.Error,
                Message = "Şəkil tapılmadı!"
            });
        }
    }
}
