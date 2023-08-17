using Figaros.Entities.DTOs.SponsorDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface ISponsorService
    {
        Task<IDataResult<SponsorDto>> Get(int SponsorId);
        Task<IDataResult<SponsorListDto>> GetAll();
        Task<IDataResult<SponsorListDto>> GetAllByNonDeleted();
        Task<IDataResult<SponsorListDto>> GetAllByDeleted();
        Task<IDataResult<SponsorDto>> Add(SponsorPostDto SponsorPostDto);
        Task<IDataResult<SponsorDto>> Update(SponsorUpdateDto SponsorUpdateDto);
        Task<IDataResult<SponsorDto>> Restore(int SponsorId);
        Task<IDataResult<SponsorDto>> Delete(int SponsorId);
        Task<IDataResult<SponsorDto>> HardDelete(int SponsorId);
    }
}
