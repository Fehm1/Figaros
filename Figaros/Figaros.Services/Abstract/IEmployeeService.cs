using Figaros.Entities.DTOs.AboutDtos;
using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<IDataResult<EmployeeDto>> Get(int employeeId);
        Task<IDataResult<EmployeeUpdateDto>> GetUpdateDto(int EmployeeId);
        Task<IDataResult<EmployeeListDto>> GetAll();
        Task<IDataResult<EmployeeListDto>> GetAllByNonDeleted();
        Task<IDataResult<EmployeeListDto>> GetAllByDeleted();
        Task<IDataResult<EmployeeDto>> Add(EmployeePostDto employeePostDto);
        Task<IDataResult<EmployeeDto>> Update(EmployeeUpdateDto employeeUpdateDto);
        Task<IDataResult<EmployeeDto>> Restore(int employeeId);
        Task<IDataResult<EmployeeDto>> Delete(int employeeId);
        Task<IDataResult<EmployeeDto>> HardDelete(int employeeId);
    }
}
