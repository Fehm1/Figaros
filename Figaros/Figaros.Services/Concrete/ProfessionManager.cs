using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.FAQDtos;
using Figaros.Entities.DTOs.ProfessionDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;

namespace Figaros.Services.Concrete
{
    public class ProfessionManager : IProfessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FAQManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ProfessionDto>> Add(ProfessionPostDto ProfessionPostDto)
        {
            if (ProfessionPostDto != null)
            {
                var profession = _mapper.Map<Profession>(ProfessionPostDto);

                var addedProfession = await _unitOfWork.Professions.AddAsync(profession);
                await _unitOfWork.SaveAsync();
                ProfessionDto professionDto = _mapper.Map<ProfessionDto>(profession);
                return new DataResult<ProfessionDto>(ResultStatus.Success, "Vəzifə uğurla əlavə edildi!", new ProfessionDto
                {
                    Profession = addedProfession,
                    ResultStatus = ResultStatus.Success,
                    Message = "Vəzifə uğurla əlavə edildi!"
                });
            }

            return new DataResult<ProfessionDto>(ResultStatus.Error, "Vəzifə əlavə edilmədi!", new ProfessionDto
            {
                Profession = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vəzifə əlavə edilmədi!"
            });
        }

        public async Task<IDataResult<ProfessionDto>> Delete(int ProfessionId)
        {
            var profession = await _unitOfWork.Professions.GetAsync(x => x.Id == ProfessionId);

            if (profession != null)
            {
                profession.IsDeleted = true;
                var deletedProfession = await _unitOfWork.Professions.UpdateAsync(profession);
                await _unitOfWork.SaveAsync();

                return new DataResult<ProfessionDto>(ResultStatus.Success, "Vəzifə uğurla silindi!", new ProfessionDto
                {
                    Profession = deletedProfession,
                    ResultStatus = ResultStatus.Success,
                    Message = "Vəzifə uğurla silindi!"
                });
            }

            return new DataResult<ProfessionDto>(ResultStatus.Error, "Vəzifə tapılmadı!", new ProfessionDto
            {
                Profession = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vəzifə tapılmadı!"
            });
        }

        public async Task<IDataResult<ProfessionDto>> Get(int ProfessionId)
        {
            var profession = await _unitOfWork.Professions.GetAsync(x => x.Id == ProfessionId);

            if (profession != null)
            {
                return new DataResult<ProfessionDto>(ResultStatus.Success, new ProfessionDto
                {
                    Profession = profession,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProfessionDto>(ResultStatus.Error, "Vəzifə tapılmadı!", new ProfessionDto
            {
                Profession = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vəzifə tapılmadı!"
            });
        }

        public async Task<IDataResult<ProfessionListDto>> GetAll()
        {
            var professions = await _unitOfWork.Professions.GetAllAsync();

            if (professions.Count >= 0)
            {
                return new DataResult<ProfessionListDto>(ResultStatus.Success, new ProfessionListDto
                {
                    Professions = professions,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ProfessionListDto>(ResultStatus.Error, "Vəzifəlar tapılmadı!", new ProfessionListDto
            {
                Professions = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vəzifəlar tapılmadı!"
            });
        }

        public async Task<IDataResult<ProfessionListDto>> GetAllByDeleted()
        {
            var professions = await _unitOfWork.Professions.GetAllAsync(x => x.IsDeleted);

            if (professions.Count >= 0)
            {
                return new DataResult<ProfessionListDto>(ResultStatus.Success, new ProfessionListDto
                {
                    Professions = professions,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ProfessionListDto>(ResultStatus.Error, "Vəzifəlar tapılmadı!", new ProfessionListDto
            {
                Professions = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vəzifəlar tapılmadı!"
            });
        }

        public async Task<IDataResult<ProfessionListDto>> GetAllByNonDeleted()
        {
            var professions = await _unitOfWork.Professions.GetAllAsync(x => !x.IsDeleted);

            if (professions.Count >= 0)
            {
                return new DataResult<ProfessionListDto>(ResultStatus.Success, new ProfessionListDto
                {
                    Professions = professions,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ProfessionListDto>(ResultStatus.Error, "Vəzifəlar tapılmadı!", new ProfessionListDto
            {
                Professions = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vəzifəlar tapılmadı!"
            });
        }

        public async Task<IDataResult<ProfessionUpdateDto>> GetUpdateDto(int ProfessionId)
        {
            var result = await _unitOfWork.Professions.AnyAsync(c => c.Id == ProfessionId);
            if (result)
            {
                var profession = await _unitOfWork.Professions.GetAsync(c => c.Id == ProfessionId);
                ProfessionUpdateDto professionUpdateDto = _mapper.Map<ProfessionUpdateDto>(profession);
                return new DataResult<ProfessionUpdateDto>(ResultStatus.Success, professionUpdateDto);
            }
            else
            {
                return new DataResult<ProfessionUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<ProfessionDto>> HardDelete(int ProfessionId)
        {
            var profession = await _unitOfWork.Professions.GetAsync(x => x.Id == ProfessionId);

            if (profession != null)
            {
                var deletedProfession = await _unitOfWork.Professions.DeleteAsync(profession);
                await _unitOfWork.SaveAsync();

                return new DataResult<ProfessionDto>(ResultStatus.Success, "Vəzifə uğurla silindi!", new ProfessionDto
                {
                    Profession = deletedProfession,
                    ResultStatus = ResultStatus.Success,
                    Message = "Vəzifə uğurla silindi!"
                });
            }

            return new DataResult<ProfessionDto>(ResultStatus.Error, "Vəzifə tapılmadı!", new ProfessionDto
            {
                Profession = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vəzifə tapılmadı!"
            });
        }

        public async Task<IDataResult<ProfessionDto>> Restore(int ProfessionId)
        {
            var profession = await _unitOfWork.Professions.GetAsync(x => x.Id == ProfessionId);

            if (profession != null)
            {
                profession.IsDeleted = false;
                var deletedProfession = await _unitOfWork.Professions.UpdateAsync(profession);
                await _unitOfWork.SaveAsync();

                return new DataResult<ProfessionDto>(ResultStatus.Success, "Vəzifə uğurla geri qaytarıldı!", new ProfessionDto
                {
                    Profession = deletedProfession,
                    ResultStatus = ResultStatus.Success,
                    Message = "Vəzifə uğurla geri qaytarıldı!"
                });
            }

            return new DataResult<ProfessionDto>(ResultStatus.Error, "Vəzifə tapılmadı!", new ProfessionDto
            {
                Profession = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vəzifə tapılmadı!"
            });
        }

        public async Task<IDataResult<ProfessionDto>> Update(ProfessionUpdateDto ProfessionUpdateDto)
        {
            var profession = await _unitOfWork.Professions.GetAsync(x => x.Id == ProfessionUpdateDto.Id);

            if (profession != null)
            {
                profession.Name = ProfessionUpdateDto.Name;
                profession.IsActive = ProfessionUpdateDto.IsActive;
                profession.ModifiedDate = DateTime.Now;

                var updatedProfession = await _unitOfWork.Professions.UpdateAsync(profession);
                await _unitOfWork.SaveAsync();

                return new DataResult<ProfessionDto>(ResultStatus.Success, "Vəzifə uğurla yeniləndi!", new ProfessionDto
                {
                    Profession = updatedProfession,
                    ResultStatus = ResultStatus.Success,
                    Message = "Vəzifə uğurla yeniləndi!"
                });
            }

            return new DataResult<ProfessionDto>(ResultStatus.Error, "Vəzifə tapılmadı!", new ProfessionDto
            {
                Profession = null,
                ResultStatus = ResultStatus.Error,
                Message = "Vəzifə tapılmadı!"
            });
        }
    }
}
