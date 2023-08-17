using Figaros.Entities.DTOs.FAQDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IFAQService
    {
        Task<IDataResult<FAQDto>> Get(int FAQId);
        Task<IDataResult<FAQUpdateDto>> GetUpdateDto(int FAQId);
        Task<IDataResult<FAQListDto>> GetAll();
        Task<IDataResult<FAQListDto>> GetAllByNonDeleted();
        Task<IDataResult<FAQListDto>> GetAllByDeleted();
        Task<IDataResult<FAQDto>> Add(FAQPostDto FAQPostDto);
        Task<IDataResult<FAQDto>> Update(FAQUpdateDto FAQUpdateDto);
        Task<IDataResult<FAQDto>> Restore(int FAQId);
        Task<IDataResult<FAQDto>> Delete(int FAQId);
        Task<IDataResult<FAQDto>> HardDelete(int FAQId);
    }
}
