using Figaros.Entities.Abstract;
using Figaros.Entities.Concrete;

namespace Figaros.Entities.DTOs.EmployeeDtos
{
    public class EmployeeDto : DtoGetBase
    {
        public Employee Employee { get; set; }
    }
}
