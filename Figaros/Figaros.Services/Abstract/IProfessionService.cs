using Figaros.Entities.DTOs.ProfessionDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IProfessionService
    {
        Task<IDataResult<ProfessionDto>> Get(int ProfessionId);
        Task<IDataResult<ProfessionUpdateDto>> GetUpdateDto(int ProfessionId);
        Task<IDataResult<ProfessionListDto>> GetAll();
        Task<IDataResult<ProfessionListDto>> GetAllByNonDeleted();
        Task<IDataResult<ProfessionListDto>> GetAllByDeleted();
        Task<IDataResult<ProfessionDto>> Add(ProfessionPostDto ProfessionPostDto);
        Task<IDataResult<ProfessionDto>> Update(ProfessionUpdateDto ProfessionUpdateDto);
        Task<IDataResult<ProfessionDto>> Restore(int ProfessionId);
        Task<IDataResult<ProfessionDto>> Delete(int ProfessionId);
        Task<IDataResult<ProfessionDto>> HardDelete(int ProfessionId);
    }
}
