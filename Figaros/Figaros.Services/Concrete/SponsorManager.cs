using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.SliderDtos;
using Figaros.Entities.DTOs.SponsorDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Extentions;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace Figaros.Services.Concrete
{
    public class SponsorManager : ISponsorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SponsorManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IDataResult<SponsorDto>> Add(SponsorPostDto SponsorPostDto)
        {
            if (SponsorPostDto != null)
            {
                var sponsor = _mapper.Map<Sponsor>(SponsorPostDto);

                if (SponsorPostDto.CompanyImageFile != null)
                {
                    if (!SponsorPostDto.CompanyImageFile.IsImageContent())
                    {
                        return new DataResult<SponsorDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new SponsorDto
                        {
                            Sponsor = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!SponsorPostDto.CompanyImageFile.IsValidImageLength())
                    {
                        return new DataResult<SponsorDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new SponsorDto
                        {
                            Sponsor = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = SponsorPostDto.CompanyImageFile.SaveImage(_env.WebRootPath, "uploads/Sponsors");
                    sponsor.CompanyImageString = newImage;
                }
                else
                {
                    return new DataResult<SponsorDto>(ResultStatus.Error, "Şəkil daxil edin!", new SponsorDto
                    {
                        Sponsor = null,
                        ResultStatus = ResultStatus.Error,
                        Message = "Şəkil daxil edin!"
                    });
                }


                var addedSponsor = await _unitOfWork.Sponsors.AddAsync(sponsor);
                await _unitOfWork.SaveAsync();
                SponsorDto sponsorDto = _mapper.Map<SponsorDto>(sponsor);

                return new DataResult<SponsorDto>(ResultStatus.Success, "Sponsor uğurla əlavə olundu!", new SponsorDto
                {
                    Sponsor = addedSponsor,
                    ResultStatus = ResultStatus.Success,
                    Message = "Sponsor uğurla əlavə olundu!"
                });
            }

            return new DataResult<SponsorDto>(ResultStatus.Error, "Sponsor əlavə olunmadı!", new SponsorDto
            {
                Sponsor = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sponsor əlavə olunmadı!"
            });
        }

        public async Task<IDataResult<SponsorDto>> Delete(int SponsorId)
        {
            var sponsor = await _unitOfWork.Sponsors.GetAsync(x => x.Id == SponsorId);

            if (sponsor != null)
            {
                sponsor.IsDeleted = true;
                var deletedSponsor = await _unitOfWork.Sponsors.UpdateAsync(sponsor);
                await _unitOfWork.SaveAsync();

                return new DataResult<SponsorDto>(ResultStatus.Success, "Sponsor uğurla silindi!", new SponsorDto
                {
                    Sponsor = deletedSponsor,
                    ResultStatus = ResultStatus.Success,
                    Message = "Sponsor uğurla silindi!"
                });
            }

            return new DataResult<SponsorDto>(ResultStatus.Error, "Sponsor tapılmadı!", new SponsorDto
            {
                Sponsor = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sponsor tapılmadı!"
            });
        }

        public async Task<IDataResult<SponsorDto>> Get(int SponsorId)
        {
            var sponsor = await _unitOfWork.Sponsors.GetAsync(x => x.Id == SponsorId); 

            if (sponsor != null)
            {
                return new DataResult<SponsorDto>(ResultStatus.Success, new SponsorDto
                {
                    Sponsor = sponsor,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<SponsorDto>(ResultStatus.Error, "Sponsor tapılmadı!", new SponsorDto
            {
                Sponsor = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sponsor tapılmadı!"
            });
        }

        public async Task<IDataResult<SponsorListDto>> GetAll()
        {
            var sponsors = await _unitOfWork.Sponsors.GetAllAsync();

            if (sponsors.Count >= 0)
            {
                return new DataResult<SponsorListDto>(ResultStatus.Success, new SponsorListDto
                {
                    Sponsors = sponsors,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<SponsorListDto>(ResultStatus.Error, "Sponsorlar tapılmadı!", new SponsorListDto
            {
                Sponsors = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sponsorlar tapılmadı!"
            });
        }

        public async Task<IDataResult<SponsorListDto>> GetAllByDeleted()
        {
            var sponsors = await _unitOfWork.Sponsors.GetAllAsync(x => x.IsDeleted);

            if (sponsors.Count >= 0)
            {
                return new DataResult<SponsorListDto>(ResultStatus.Success, new SponsorListDto
                {
                    Sponsors = sponsors,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<SponsorListDto>(ResultStatus.Error, "Sponsorlar tapılmadı!", new SponsorListDto
            {
                Sponsors = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sponsorlar tapılmadı!"
            });
        }

        public async Task<IDataResult<SponsorListDto>> GetAllByNonDeleted()
        {
            var sponsors = await _unitOfWork.Sponsors.GetAllAsync(x => !x.IsDeleted);

            if (sponsors.Count >= 0)
            {
                return new DataResult<SponsorListDto>(ResultStatus.Success, new SponsorListDto
                {
                    Sponsors = sponsors,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<SponsorListDto>(ResultStatus.Error, "Sponsorlar tapılmadı!", new SponsorListDto
            {
                Sponsors = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sponsorlar tapılmadı!"
            });
        }

        public async Task<IDataResult<SponsorUpdateDto>> GetUpdateDto(int SponsorId)
        {
            var result = await _unitOfWork.Sponsors.AnyAsync(c => c.Id == SponsorId);
            if (result)
            {
                var sponsor = await _unitOfWork.Sponsors.GetAsync(c => c.Id == SponsorId);
                SponsorUpdateDto sponsorUpdateDto = _mapper.Map<SponsorUpdateDto>(sponsor);
                return new DataResult<SponsorUpdateDto>(ResultStatus.Success, sponsorUpdateDto);
            }
            else
            {
                return new DataResult<SponsorUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<SponsorDto>> HardDelete(int SponsorId)
        {
            var sponsor = await _unitOfWork.Sponsors.GetAsync(x => x.Id == SponsorId);

            if (sponsor != null)
            {
                var deletedSponsor = await _unitOfWork.Sponsors.DeleteAsync(sponsor);
                sponsor.CompanyImageString.DeleteImage(_env.WebRootPath, "uploads/Sponsors");
                await _unitOfWork.SaveAsync();

                return new DataResult<SponsorDto>(ResultStatus.Success, "Sponsor uğurla silindi!", new SponsorDto
                {
                    Sponsor = deletedSponsor,
                    ResultStatus = ResultStatus.Success,
                    Message = "Sponsor uğurla silindi!"
                });
            }

            return new DataResult<SponsorDto>(ResultStatus.Error, "Sponsor tapılmadı!", new SponsorDto
            {
                Sponsor = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sponsor tapılmadı!"
            });
        }

        public async Task<IDataResult<SponsorDto>> Restore(int SponsorId)
        {
            var sponsor = await _unitOfWork.Sponsors.GetAsync(x => x.Id == SponsorId);

            if (sponsor != null)
            {
                sponsor.IsDeleted = false;
                var deletedSponsor = await _unitOfWork.Sponsors.UpdateAsync(sponsor);
                await _unitOfWork.SaveAsync();

                return new DataResult<SponsorDto>(ResultStatus.Success, "Sponsor uğurla silindi!", new SponsorDto
                {
                    Sponsor = deletedSponsor,
                    ResultStatus = ResultStatus.Success,
                    Message = "Sponsor uğurla silindi!"
                });
            }

            return new DataResult<SponsorDto>(ResultStatus.Error, "Sponsor tapılmadı!", new SponsorDto
            {
                Sponsor = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sponsor tapılmadı!"
            });
        }

        public async Task<IDataResult<SponsorDto>> Update(SponsorUpdateDto SponsorUpdateDto)
        {
            var sponsor = await _unitOfWork.Sponsors.GetAsync(x => x.Id == SponsorUpdateDto.Id);

            if (sponsor != null)
            {
                if (SponsorUpdateDto.CompanyImageFile != null)
                {
                    if (!SponsorUpdateDto.CompanyImageFile.IsImageContent())
                    {
                        return new DataResult<SponsorDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new SponsorDto
                        {
                            Sponsor = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!SponsorUpdateDto.CompanyImageFile.IsValidImageLength())
                    {
                        return new DataResult<SponsorDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new SponsorDto
                        {
                            Sponsor = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = SponsorUpdateDto.CompanyImageFile.SaveImage(_env.WebRootPath, "uploads/Sponsors");
                    sponsor.CompanyImageString.DeleteImage(_env.WebRootPath, "uploads/Sponsors");

                    sponsor.CompanyImageString = newImage;
                }

                sponsor.CompanyName = SponsorUpdateDto.CompanyName;
                sponsor.IsActive = SponsorUpdateDto.IsActive;
                sponsor.ModifiedDate = DateTime.Now;

                var updatedSponsor = await _unitOfWork.Sponsors.UpdateAsync(sponsor);
                await _unitOfWork.SaveAsync();

                return new DataResult<SponsorDto>(ResultStatus.Success, "Sponsor uğurla yeniləndi!", new SponsorDto
                {
                    Sponsor = updatedSponsor,
                    ResultStatus = ResultStatus.Error,
                    Message = "Sponsor uğurla yeniləndi!"
                });
            }

            return new DataResult<SponsorDto>(ResultStatus.Error, "Sponsor tapılmadı!", new SponsorDto
            {
                Sponsor = null,
                ResultStatus = ResultStatus.Error,
                Message = "Sponsor tapılmadı!"
            });
        }
    }
}
