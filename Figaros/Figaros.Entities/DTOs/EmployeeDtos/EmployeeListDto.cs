using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.EmployeeDtos
{
    public class EmployeeListDto : DtoGetBase
    {
        public IList<Employee> Employees { get; set; }
    }
}
