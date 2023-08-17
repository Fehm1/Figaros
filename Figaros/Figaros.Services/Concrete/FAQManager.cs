using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.BookingDtos;
using Figaros.Entities.DTOs.FAQDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;

namespace Figaros.Services.Concrete
{
    public class FAQManager : IFAQService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FAQManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<FAQDto>> Add(FAQPostDto FAQPostDto)
        {
            if (FAQPostDto != null)
            {
                var faq = _mapper.Map<FAQ>(FAQPostDto);

                var addedFAQ = await _unitOfWork.FAQs.AddAsync(faq);
                await _unitOfWork.SaveAsync();
                FAQDto FAQDto = _mapper.Map<FAQDto>(faq);
                return new DataResult<FAQDto>(ResultStatus.Success, "Sual uğurla əlavə edildi!", new FAQDto
                {
                    FAQ = addedFAQ,
                    ResultStatus = ResultStatus.Success,
                    Message = "Sual uğurla əlavə edildi!"
                });
            }

            return new DataResult<FAQDto>(ResultStatus.Error, "Sual əlavə edilmədi!", new FAQDto
            {
                FAQ = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sual əlavə edilmədi!"
            });
        }

        public async Task<IDataResult<FAQDto>> Delete(int FAQId)
        {
            var FAQ = await _unitOfWork.FAQs.GetAsync(x => x.Id == FAQId);

            if (FAQ != null)
            {
                FAQ.IsDeleted = true;
                var deletedFAQ = await _unitOfWork.FAQs.UpdateAsync(FAQ);
                await _unitOfWork.SaveAsync();

                return new DataResult<FAQDto>(ResultStatus.Success, "Sual uğurla silindi!", new FAQDto
                {
                    FAQ = deletedFAQ,
                    ResultStatus = ResultStatus.Success,
                    Message = "Sual uğurla silindi!"
                });
            }

            return new DataResult<FAQDto>(ResultStatus.Error, "Sual tapılmadı!", new FAQDto
            {
                FAQ = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sual tapılmadı!"
            });
        }

        public async Task<IDataResult<FAQDto>> Get(int FAQId)
        {
            var FAQ = await _unitOfWork.FAQs.GetAsync(x => x.Id == FAQId);

            if (FAQ != null)
            {
                return new DataResult<FAQDto>(ResultStatus.Success, new FAQDto
                {
                    FAQ = FAQ,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<FAQDto>(ResultStatus.Error, "Sual tapılmadı!", new FAQDto
            {
                FAQ = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sual tapılmadı!"
            });
        }

        public async Task<IDataResult<FAQListDto>> GetAll()
        {
            var FAQs = await _unitOfWork.FAQs.GetAllAsync();

            if (FAQs.Count >= 0)
            {
                return new DataResult<FAQListDto>(ResultStatus.Success, new FAQListDto
                {
                    FAQs = FAQs,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<FAQListDto>(ResultStatus.Error, "Suallar tapılmadı!", new FAQListDto
            {
                FAQs = null,
                ResultStatus = ResultStatus.Error,
                Message = "Suallar tapılmadı!"
            });
        }

        public async Task<IDataResult<FAQListDto>> GetAllByDeleted()
        {
            var FAQs = await _unitOfWork.FAQs.GetAllAsync(x => x.IsDeleted);

            if (FAQs.Count >= 0)
            {
                return new DataResult<FAQListDto>(ResultStatus.Success, new FAQListDto
                {
                    FAQs = FAQs,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<FAQListDto>(ResultStatus.Error, "Suallar tapılmadı!", new FAQListDto
            {
                FAQs = null,
                ResultStatus = ResultStatus.Error,
                Message = "Suallar tapılmadı!"
            });
        }

        public async Task<IDataResult<FAQListDto>> GetAllByNonDeleted()
        {
            var FAQs = await _unitOfWork.FAQs.GetAllAsync(x => !x.IsDeleted );

            if (FAQs.Count >= 0)
            {
                return new DataResult<FAQListDto>(ResultStatus.Success, new FAQListDto
                {
                    FAQs = FAQs,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<FAQListDto>(ResultStatus.Error, "Suallar tapılmadı!", new FAQListDto
            {
                FAQs = null,
                ResultStatus = ResultStatus.Error,
                Message = "Suallar tapılmadı!"
            });
        }

        public async Task<IDataResult<FAQUpdateDto>> GetUpdateDto(int FAQId)
        {
            var result = await _unitOfWork.FAQs.AnyAsync(c => c.Id == FAQId);
            if (result)
            {
                var FAQ = await _unitOfWork.FAQs.GetAsync(c => c.Id == FAQId);
                FAQUpdateDto FAQUpdateDto = _mapper.Map<FAQUpdateDto>(FAQ);
                return new DataResult<FAQUpdateDto>(ResultStatus.Success, FAQUpdateDto);
            }
            else
            {
                return new DataResult<FAQUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<FAQDto>> HardDelete(int FAQId)
        {
            var FAQ = await _unitOfWork.FAQs.GetAsync(x => x.Id == FAQId);

            if (FAQ != null)
            {
                var deletedFAQ = await _unitOfWork.FAQs.DeleteAsync(FAQ);
                await _unitOfWork.SaveAsync();

                return new DataResult<FAQDto>(ResultStatus.Success, "Sual uğurla silindi!", new FAQDto
                {
                    FAQ = deletedFAQ,
                    ResultStatus = ResultStatus.Success,
                    Message = "Sual uğurla silindi!"
                });
            }

            return new DataResult<FAQDto>(ResultStatus.Error, "Sual tapılmadı!", new FAQDto
            {
                FAQ = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sual tapılmadı!"
            });
        }

        public async Task<IDataResult<FAQDto>> Restore(int FAQId)
        {
            var FAQ = await _unitOfWork.FAQs.GetAsync(x => x.Id == FAQId);

            if (FAQ != null)
            {
                FAQ.IsDeleted = false;
                var restoredFAQ = await _unitOfWork.FAQs.UpdateAsync(FAQ);
                await _unitOfWork.SaveAsync();

                return new DataResult<FAQDto>(ResultStatus.Success, "Sual uğurla geri qaytarıldı!", new FAQDto
                {
                    FAQ = restoredFAQ,
                    ResultStatus = ResultStatus.Success,
                    Message = "Sual uğurla geri qaytarıldı!"
                });
            }

            return new DataResult<FAQDto>(ResultStatus.Error, "Sual tapılmadı!", new FAQDto
            {
                FAQ = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sual tapılmadı!"
            });
        }

        public async Task<IDataResult<FAQDto>> Update(FAQUpdateDto FAQUpdateDto)
        {
            var FAQ = await _unitOfWork.FAQs.GetAsync(x => x.Id == FAQUpdateDto.Id);

            if (FAQ != null)
            {
                FAQ.Question = FAQUpdateDto.Question;
                FAQ.Answer = FAQUpdateDto.Answer;
                FAQ.ModifiedDate = DateTime.Now;

                var updatedFAQ = await _unitOfWork.FAQs.UpdateAsync(FAQ);
                await _unitOfWork.SaveAsync();

                return new DataResult<FAQDto>(ResultStatus.Success, "Sual uğurla yeniləndi!", new FAQDto
                {
                    FAQ = updatedFAQ,
                    ResultStatus = ResultStatus.Success,
                    Message = "Sual uğurla yeniləndi!"
                });
            }

            return new DataResult<FAQDto>(ResultStatus.Error, "Sual tapılmadı!", new FAQDto
            {
                FAQ = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sual tapılmadı!"
            });
        }
    }
}
